using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructue.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdvertController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<StoreContext> _unitOfWork;
        private readonly IUploadService _uploadService;
        public AdvertController(IUnitOfWork<StoreContext> unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _uploadService = uploadService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdvertDto>>> GetAdverts()
        {
            var spec = new BaseSpecification<Advert>();

            var Adverts = await _unitOfWork.Repository<Advert, StoreContext>().ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<AdvertDto>>(Adverts));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertDto>> GetAdvert(int id)
        {
            var Advert = await  _unitOfWork.Repository<Advert, StoreContext>().GetByIdAsync(id);

            if (Advert == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Advert, AdvertDto>(Advert);
        }

        [HttpPost]
        public async Task<ActionResult<AdvertCDto>> CreateAdvert(AdvertCDto advertDto)
        {
            var advert = _mapper.Map<AdvertCDto, Advert>(advertDto);
            advert.GymId = User.GetGymId();
            
             _unitOfWork.Repository<Advert, StoreContext>().Add(advert);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Failed to Create Advert"));

            return Ok(advert);
        }

        [HttpPost("Upload/{id}")]
        public async Task<ActionResult> Upload(IFormFile file, int id)
        {
            var advert = await  _unitOfWork.Repository<Advert, StoreContext>().GetByIdAsync(id);

            if (advert == null) return NotFound(new ApiResponse(404));

            var uploadFile = await _uploadService.UploadAsync(file, "images/adverts");

            advert.ImageUrl = "images/adverts/" + uploadFile.FileName;

            _unitOfWork.Repository<Advert, StoreContext>().Update(advert);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Advert"));
            
            return Ok(new { uploadFile.FileName });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var advert = await  _unitOfWork.Repository<Advert, StoreContext>().GetByIdAsync(id);

            if (advert == null) return NotFound(new ApiResponse(404));

             _unitOfWork.Repository<Advert, StoreContext>().Delete(advert);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Failed to delete Advert"));

            return NoContent();
        }

    }
}