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
    public class NotificationController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<StoreContext> _unitOfWork;
        public NotificationController(IUnitOfWork<StoreContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotificationDto>>> GetNotifications()
        {
            var spec = new BaseSpecification<Notification>();

            var notifications = await _unitOfWork.Repository<Notification, StoreContext>().ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<NotificationDto>>(notifications));
        }
        
        [HttpGet("User")]
        public async Task<ActionResult<List<NotificationDto>>> GetUserNotifications()
        {
            var spec = new BaseSpecification<Notification>(x => x.AppUserId == User.GetUserId() || x.AppUserId == ""); 

            var notifications = await _unitOfWork.Repository<Notification, StoreContext>().ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<NotificationDto>>(notifications));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NotificationDto>> GetNotification(int id)
        {
            var spec = new BaseSpecification<Notification>(x => x.Id == id);

            var notification = await _unitOfWork.Repository<Notification, StoreContext>().GetEntityWithSpec(spec);

            if (notification == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Notification, NotificationDto>(notification);
        }

        [HttpPost]
        public async Task<ActionResult<NotificationCDto>> CreateNotification(NotificationCDto notificationDto)
        {
            var notification = _mapper.Map<NotificationCDto, Notification>(notificationDto);
            
            _unitOfWork.Repository<Notification, StoreContext>().Add(notification);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest("Failed to create notification");

            return Ok(notification);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _unitOfWork.Repository<Notification, StoreContext>().GetByIdAsync(id);

            if (notification == null) return NotFound();

            _unitOfWork.Repository<Notification, StoreContext>().Delete(notification, true);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return BadRequest("Failed to delete notification");

            return NoContent();
        }

    }
}