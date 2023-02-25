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
        private readonly StoreContext _context;
        private readonly IGenericRepository<Notification, StoreContext> _notificationRepo;
        private readonly IMapper _mapper;
        public NotificationController(StoreContext context, IGenericRepository<Notification, StoreContext> notificationRepo, IMapper mapper)
        {
            _mapper = mapper;
            _notificationRepo = notificationRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<NotificationDto>>> GetNotifications()
        {
            var spec = new BaseSpecification<Notification>();

            var notifications = await _notificationRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<NotificationDto>>(notifications));
        }
        [HttpGet("User")]
        public async Task<ActionResult<List<NotificationDto>>> GetUserNotifications()
        {
            var spec = new BaseSpecification<Notification>(x => x.AppUserId == User.GetUserId());

            var notifications = await _notificationRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<NotificationDto>>(notifications));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NotificationDto>> GetNotification(int id)
        {
            var spec = new BaseSpecification<Notification>(x => x.Id == id);

            var notification = await _notificationRepo.GetEntityWithSpec(spec);

            if (notification == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Notification, NotificationDto>(notification);
        }

        [HttpPost]
        public async Task<ActionResult<NotificationCDto>> CreateNotification(NotificationCDto notificationDto)
        {
            var notification = _mapper.Map<NotificationCDto, Notification>(notificationDto);
            _notificationRepo.Add(notification);

            var result = await _context.SaveChangesAsync();

            if (result <= 0) return BadRequest("Failed to create notification");

            return Ok(notification);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _notificationRepo.GetByIdAsync(id);

            if (notification == null) return NotFound();

            _notificationRepo.Delete(notification);

            var result = await _context.SaveChangesAsync();

            if (result <= 0) return BadRequest("Failed to delete notification");

            return NoContent();
        }

    }
}