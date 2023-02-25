using System.Security.Claims;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Age = user.DateOfBirth.CalcuateAge(),
                Hieght = user.Hieght,
                Wieght = user.Wieght,
                PhotoUrl = user.PhotoUrl,
                SubscriptionExpDate = user.SubscriptionExpDate,
                SubscriptionTypeId = user.SubscriptionTypeId,
                GymId = user.GymId
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var userdto = _mapper.Map<AppUser, UserDto>(user);
            userdto.Token = _tokenService.CreateToken(user);
            return userdto;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            var userdto = _mapper.Map<AppUser, UserDto>(user);
            userdto.Token = _tokenService.CreateToken(user);
            return userdto;
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("Subscription")]
        public async Task<ActionResult<SubscriptionDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

            return _mapper.Map<SubscriptionType, SubscriptionDto>(user.SubscriptionType);
        }

        // [HttpGet("address")]
        // public async Task<ActionResult<SubscriptionDto>> GetUserAddress()
        // {
        //     var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

        //     return _mapper.Map<Address, AddressDto>(user.Address);
        // }

        // [Authorize]
        // [HttpPut("address")]
        // public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        // {
        //     var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);

        //     user.Address = _mapper.Map<AddressDto, Address>(address);

        //     var result = await _userManager.UpdateAsync(user);

        //     if (result.Succeeded) return Ok(_mapper.Map<AddressDto>(user.Address));

        //     return BadRequest("Problem updating the user");
        // }
    }
}