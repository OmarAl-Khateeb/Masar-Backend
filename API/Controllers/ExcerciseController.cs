using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<CourseContext> _unitOfWork;
        public ExcerciseController(IUnitOfWork<CourseContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExcerciseDto>>> GetExcercises()
        {
            var spec = new BaseSpecification<Excercise>();

            var excercises = await _unitOfWork.Repository<Excercise, CourseContext>().ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<ExcerciseDto>>(excercises));
        }

        [HttpPost]
        public async Task<ActionResult<ExcercisePlan>> CreateExcercisePlan(ExcerciseCPlanDto excercisePlanDto)
        {
            var excercisePlan = _mapper.Map<ExcerciseCPlanDto, ExcercisePlan>(excercisePlanDto);

            int index = 1;

            excercisePlan.ExcerciseList.ForEach(excerciseSet => excerciseSet.Index = index++);
            
            _unitOfWork.Repository<ExcercisePlan, CourseContext>().Add(excercisePlan);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Failed to create Excercise plan"));

            return Ok(excercisePlan);
        }
        
        [HttpGet("Plans")]
        public async Task<ActionResult<IReadOnlyList<ExcercisePlanDto>>> GetExcercisePlans(string appUserId, int? day)
        {
            var spec = new ExcercisePlanSpecification(appUserId, day);

            var ExcercisePlans = await _unitOfWork.Repository<ExcercisePlan, CourseContext>().ListAsync(spec);

            if (ExcercisePlans == null) return NotFound(new ApiResponse(404));

            var ExcercisePlanDtos = _mapper.Map<IReadOnlyList<ExcercisePlan>, IReadOnlyList<ExcercisePlanDto>>(ExcercisePlans);

            return Ok(ExcercisePlanDtos);
        }

        
        [HttpGet("UserPlans")]
        public async Task<ActionResult<IReadOnlyList<ExcercisePlanDto>>> GetExcercisePlansByUserId(int? day)
        {
            var spec = new ExcercisePlanSpecification(User.GetUserId(), day);

            var ExcercisePlans = await _unitOfWork.Repository<ExcercisePlan, CourseContext>().ListAsync(spec);

            if (ExcercisePlans == null) return NotFound(new ApiResponse(404));

            var ExcercisePlanDtos = _mapper.Map<IReadOnlyList<ExcercisePlan>, IReadOnlyList<ExcercisePlanDto>>(ExcercisePlans);

            return Ok(ExcercisePlanDtos);
        }

    }
}