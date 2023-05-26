using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivityController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public ActivityController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ActivityDto>>> GetActivitys(
            [FromQuery] ActivitySpecParams ActivityParams)
        {
            var spec = new ActivitySpecification(ActivityParams);
            var countSpec = new ActivitySpecification(ActivityParams);

            var totalItems = await _unitOfWork.Repository<Activity>().CountAsync(countSpec);
            var Activitys = await _unitOfWork.Repository<Activity>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ActivityDto>>(Activitys);
            var pageData = new Pagination<ActivityDto>(ActivityParams.PageIndex, ActivityParams.PageSize, totalItems, data);

            return Ok(new ApiResponse(200, pageData));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            var Activity = await _unitOfWork.Repository<Activity>().GetByIdAsync(id);

            if (Activity == null) return NotFound(new ApiResponse(404));

            return Ok(new ApiResponse(200, _mapper.Map<Activity, ActivityDto>(Activity)));
        }

        [HttpPost]
        public async Task<ActionResult<ActivityDto>> CreateActivity([FromForm] ActivityCDto ActivityCDto)
        {
            var Activity = _mapper.Map<ActivityCDto, Activity>(ActivityCDto);

            Activity.Student = await _unitOfWork.Repository<Student>().GetByIdAsync(ActivityCDto.StudentId);

            if (ActivityCDto.Files != null && ActivityCDto.Files.Count > 0)
            {
                List<Document> documents = new List<Document>();

                foreach (var file in ActivityCDto.Files)
                {
                    // Upload each file and create a document
                    Document document = await _imageService.UploadDocumentAsync(file, "students/documents");
                    documents.Add(document);
                }

                Activity.Documents = documents;
            }

            _unitOfWork.Repository<Activity>().Add(Activity);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Activity"));

            return Created("test", new ApiResponse(201, _mapper.Map<Activity, ActivityDto>(Activity)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ActivityDto>> UpdateActivity(int id, ActivityCDto ActivityCDto)
        {
            var Activity = await _unitOfWork.Repository<Activity>().GetByIdAsync(id);

            if (Activity == null) return NotFound(new ApiResponse(404));

            _mapper.Map<ActivityCDto, Activity>(ActivityCDto, Activity);

            _unitOfWork.Repository<Activity>().Update(Activity);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Activity"));

            return Ok(new ApiResponse(200, _mapper.Map<Activity, ActivityDto>(Activity)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActivity(int id)
        {
            var Activity = await _unitOfWork.Repository<Activity>().GetByIdAsync(id);

            if (Activity == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Activity>().Delete(Activity);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Activity"));

            return NoContent();
        }
    }

}