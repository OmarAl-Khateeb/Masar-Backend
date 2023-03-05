using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<UserDto>>> GetUsers([FromQuery] AppUserSpecParams specParams)
        {
            var spec = new AppUserSpecification(specParams);
            var countSpec = new AppUserSpecification(specParams);

            var totalItems = await _userManager.Users
                .Where(countSpec.Criteria)
                .CountAsync();

            var users = await _userManager.Users
                .Include(u => u.SubscriptionType)
                .Where(spec.Criteria)
                .OrderBy(countSpec.OrderBy)
                // .OrderByDescending(countSpec.OrderByDescending) doesn't work
                .Skip(spec.Skip).Take(spec.Take)//paging
                .ToListAsync();

            var data = _mapper.Map<IReadOnlyList<UserDto>>(users);

            return Ok(new Pagination<UserDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

    }
}