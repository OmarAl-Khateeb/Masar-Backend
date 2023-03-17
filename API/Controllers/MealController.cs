using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Meal;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class MealController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<CourseContext> _unitOfWork;
        public MealController(IUnitOfWork<CourseContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MealDto>>> GetMeals()
        {
            var spec = new BaseSpecification<Meal>();

            var meals = await _unitOfWork.Repository<Meal, CourseContext>().ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<MealDto>>(meals));
        }

        [HttpPost]
        public async Task<ActionResult<MealPlan>> CreateMealPlan(MealCPlanDto mealPlanDto)
        {
            var mealPlan = _mapper.Map<MealCPlanDto, MealPlan>(mealPlanDto);

            int index = 1;

            mealPlan.MealList.ForEach(meal => meal.Index = index++);
            _unitOfWork.Repository<MealPlan, CourseContext>().Add(mealPlan);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Failed to create meal plan"));

            return Ok(mealPlan);
        }

        
        [HttpGet("Plans")]
        public async Task<ActionResult<IReadOnlyList<MealPlanDto>>> GetMealPlans(string appUserId, int? day)
        {
            var spec = new MealPlanSpecification(appUserId, day);

            var mealPlans = await _unitOfWork.Repository<MealPlan, CourseContext>().ListAsync(spec);
            
            if (mealPlans == null) return NotFound(new ApiResponse(404));

            var mealPlanDtos = _mapper.Map<IReadOnlyList<MealPlan>, IReadOnlyList<MealPlanDto>>(mealPlans);

            return Ok(mealPlanDtos);
        }

        
        [HttpGet("UserPlans")]
        public async Task<ActionResult<IReadOnlyList<MealPlanDto>>> GetMealPlansByUserId()
        {
            var spec = new MealPlanSpecification(User.GetUserId());

            var mealPlans = await _unitOfWork.Repository<MealPlan, CourseContext>().ListAsync(spec);

            if (mealPlans == null) return NotFound(new ApiResponse(404));

            var mealPlanDtos = _mapper.Map<IReadOnlyList<MealPlan>, IReadOnlyList<MealPlanDto>>(mealPlans);

            return Ok(mealPlanDtos);
        }

    }
}