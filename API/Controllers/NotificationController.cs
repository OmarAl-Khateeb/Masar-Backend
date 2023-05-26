using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotificationController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<NotificationDto>>> GetNotifications(
            [FromQuery] NotificationSpecParams NotificationParams)
        {
            var spec = new NotificationSpecification(NotificationParams);
            var countSpec = new NotificationSpecification(NotificationParams);

            var totalItems = await _unitOfWork.Repository<Notification>().CountAsync(countSpec);
            var Notifications = await _unitOfWork.Repository<Notification>().ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<NotificationDto>>(Notifications);
            var pageData = new Pagination<NotificationDto>(NotificationParams.PageIndex,
                NotificationParams.PageSize, totalItems, data);
            return Ok(new ApiResponse(200, pageData));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetNotification(int id)
        {
            var Notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);

            if (Notification == null) return NotFound(new ApiResponse(404));

            var NotificationDto =  _mapper.Map<Notification, NotificationDto>(Notification);

            return Ok(new ApiResponse(200, NotificationDto));
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotification([FromForm] NotificationCDto NotificationCDto)
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            var Notification = _mapper.Map<NotificationCDto, Notification>(NotificationCDto);

            Notification.Student = await _unitOfWork.Repository<Student>().GetByIdAsync(NotificationCDto.StudentId);
            Notification.User = user;

            if (NotificationCDto.File != null) Notification.Document = await _imageService.UploadDocumentAsync(NotificationCDto.File, "students/documents");

            _unitOfWork.Repository<Notification>().Add(Notification);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Creating Notification"));

            var NotificationDto =  _mapper.Map<Notification, NotificationDto>(Notification);

            return Created("test", new ApiResponse(201, NotificationDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Notification>> UpdateNotification(int id, NotificationCDto NotificationCDto)
        {
            var Notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);

            if (Notification == null) return NotFound(new ApiResponse(404));

            _mapper.Map<NotificationCDto, Notification>(NotificationCDto, Notification);

            _unitOfWork.Repository<Notification>().Update(Notification);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Updating Notification"));

            return Ok(new ApiResponse(200, Notification));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotification(int id)
        {
            var Notification = await _unitOfWork.Repository<Notification>().GetByIdAsync(id);

            if (Notification == null) return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Notification>().Delete(Notification);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest(new ApiResponse(400, "Problem Deleting Notification"));

            return NoContent();
        }
    }
}