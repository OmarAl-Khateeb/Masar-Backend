using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using AutoMapper;
using Core.Entities.Meal;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data.Course;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly IGenericRepository<MealPlan, CourseContext> _mealPlansRepo;
        private readonly IGenericRepository<Meal, CourseContext> _mealsRepo;
        private readonly IMapper _mapper;
        private readonly CourseContext _context;
        public CourseController(CourseContext context, IGenericRepository<MealPlan, CourseContext> mealPlansRepo, IGenericRepository<Meal, CourseContext> mealsRepo, IMapper mapper)
        {
            _context = context;
            _mealPlansRepo = mealPlansRepo;
            _mapper = mapper;
            _mealsRepo = mealsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<MealDto>>> GetMeals()
        {
            var spec = new MealsSpecification();

            var meals = await _mealsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<MealDto>>(meals));
        }

        [HttpPost]
        public async Task<ActionResult<MealPlan>> CreateMealPlan(MealPlanDto mealPlanDto)
        {
        var mealPlan = _mapper.Map<MealPlanDto, MealPlan>(mealPlanDto);

        // var spec = new MealPlanSpecification(mealPlan.AppUserId, mealPlan.Day);

        // var planExist = await _mealPlansRepo.ListAsync(spec);

        // if (planExist != null) return NoContent(); //update method?

        foreach (var mealDto in mealPlanDto.MealList)
        {
            var meal = _mapper.Map<MealDto, Meal>(mealDto);
            
        }

        mealPlan.MealList.ForEach(meal => meal.AppUserId = mealPlanDto.AppUserId);
        _mealPlansRepo.Add(mealPlan);

        var result = await _context.SaveChangesAsync();

        if (result <= 0) return BadRequest("Failed to create meal plan");

        return Ok(mealPlan);
        }

        
        [HttpGet("Plans")]
        public async Task<ActionResult<IReadOnlyList<MealPlanDto>>> GetMealPlans(string appUserId, int? day)
        {
            var spec = new MealPlanSpecification(appUserId, day);

            var mealPlans = await _mealPlansRepo.ListAsync(spec);

            var mealPlanDtos = _mapper.Map<IReadOnlyList<MealPlan>, IReadOnlyList<MealPlanDto>>(mealPlans);

            return Ok(mealPlanDtos);
        }

        
        [HttpGet("UserPlans")]
        public async Task<ActionResult<IReadOnlyList<MealPlanDto>>> GetMealPlansByUserId(int? day)
        {
            var spec = new MealPlanSpecification(User.GetUserId(), day);

            var mealPlans = await _mealPlansRepo.ListAsync(spec);

            var mealPlanDtos = _mapper.Map<IReadOnlyList<MealPlan>, IReadOnlyList<MealPlanDto>>(mealPlans);

            return Ok(mealPlanDtos);
        }

    }
}