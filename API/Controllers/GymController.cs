using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructue.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class GymsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<StoreContext> _unitOfWork;

        public GymsController(IUnitOfWork<StoreContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GymDto>>> GetGyms()
        {
            var gyms = await _unitOfWork.Repository<Gym, StoreContext>().ListAllAsync();

            var gymDtos = _mapper.Map<IReadOnlyList<Gym>, IReadOnlyList<GymDto>>(gyms);

            return Ok(gymDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GymDto>> GetGym(int id)
        {
            var gym = await _unitOfWork.Repository<Gym, StoreContext>().GetByIdAsync(id);

            if (gym == null) return NotFound(new ApiResponse(404));

            var gymDto = _mapper.Map<Gym, GymDto>(gym);

            return Ok(gymDto);
        }

        [HttpPost]
        public async Task<ActionResult<GymCDto>> CreateGym(GymCDto gymDto)
        {
            var gym = _mapper.Map<GymCDto, Gym>(gymDto);

            _unitOfWork.Repository<Gym, StoreContext>().Add(gym);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Failed to create gym"));

            return Ok(gym);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GymCDto>> UpdateGym(int id, GymCDto gymDto)
        {
            var gym = await _unitOfWork.Repository<Gym, StoreContext>().GetByIdAsync(id);

            if (gym == null) return NotFound(new ApiResponse(404));

            _mapper.Map(gymDto, gym);

            _unitOfWork.Repository<Gym, StoreContext>().Update(gym);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Failed to update gym"));

            return Ok(_mapper.Map<Gym, GymCDto>(gym));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGym(int id)
        {
            var gym = await _unitOfWork.Repository<Gym, StoreContext>().GetByIdAsync(id);

            if (gym == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Gym, StoreContext>().Delete(gym);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Failed to delete gym"));

            return NoContent();
        }
    }
}
