using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using AutoMapper;
using Core.Entities.Excercise;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data.Course;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ExcerciseController : BaseApiController
    {
        private readonly IGenericRepository<ExcercisePlan, CourseContext> _excercisePlansRepo;
        private readonly IGenericRepository<ExcerciseSet, CourseContext> _excerciseSetsRepo;
        private readonly IGenericRepository<Excercise, CourseContext> _excercisesRepo;
        private readonly IMapper _mapper;
        private readonly CourseContext _context;
        public ExcerciseController(CourseContext context, 
            IGenericRepository<ExcercisePlan, CourseContext> excercisePlansRepo, 
            IGenericRepository<Excercise, CourseContext> excercisesRepo,
            IGenericRepository<ExcerciseSet, CourseContext> excerciseSetsRepo, IMapper mapper)
        {
            _context = context;
            _excercisePlansRepo = excercisePlansRepo;
            _mapper = mapper;
            _excerciseSetsRepo = excerciseSetsRepo;
            _excercisesRepo = excercisesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExcerciseDto>>> GetExcercises()
        {
            var spec = new BaseSpecification<Excercise>();

            var excercises = await _excercisesRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<ExcerciseDto>>(excercises));
        }

        [HttpPost]
        public async Task<ActionResult<ExcercisePlan>> CreateExcercisePlan(ExcerciseCPlanDto excercisePlanDto)
        {
            var excercisePlan = _mapper.Map<ExcerciseCPlanDto, ExcercisePlan>(excercisePlanDto);

            int index = 1;

            excercisePlan.Excerciselist.ForEach(excerciseSet => excerciseSet.Index = index++);
            _excercisePlansRepo.Add(excercisePlan);

            var result = await _context.SaveChangesAsync();

            if (result <= 0) return BadRequest("Failed to create Excercise plan");

            return Ok(excercisePlan);
        }

        
        [HttpGet("Plans")]
        public async Task<ActionResult<IReadOnlyList<ExcercisePlanDto>>> GetExcercisePlans(string appUserId, int? day)
        {
            var spec = new ExcercisePlanSpecification(appUserId, day);

            var ExcercisePlans = await _excercisePlansRepo.ListAsync(spec);

            var ExcercisePlanDtos = _mapper.Map<IReadOnlyList<ExcercisePlan>, IReadOnlyList<ExcercisePlanDto>>(ExcercisePlans);

            return Ok(ExcercisePlanDtos);
        }

        
        [HttpGet("UserPlans")]
        public async Task<ActionResult<IReadOnlyList<ExcercisePlanDto>>> GetExcercisePlansByUserId(int? day)
        {
            var spec = new ExcercisePlanSpecification(User.GetUserId(), day);

            var ExcercisePlans = await _excercisePlansRepo.ListAsync(spec);

            var ExcercisePlanDtos = _mapper.Map<IReadOnlyList<ExcercisePlan>, IReadOnlyList<ExcercisePlanDto>>(ExcercisePlans);

            return Ok(ExcercisePlanDtos);
        }

    }
}