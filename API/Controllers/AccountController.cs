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
            var user = await _userManager.FindUserByClaimsPrincipleWithSubscription(User);

            return _mapper.Map<AppUser, UserDto>(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var userdto = _mapper.Map<AppUser, UserTokenDto>(user);

            userdto.Token = _tokenService.CreateToken(user);

            return userdto;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserTokenDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DateOfBirth = DateTime.Today.AddYears(-30),
                Height = 180,
                Weight = 100,
                PhotoUrl = "some/some",
                GymId = 1,
                SubscriptionExpDate = DateTime.UtcNow.AddMonths(2),
                SubscriptionTypeId = 1
                //temporary
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            var userdto = _mapper.Map<AppUser, UserTokenDto>(user);
            userdto.Token = _tokenService.CreateToken(user);
            return userdto;
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("Subscription")]
        public async Task<ActionResult<SubscriptionDto>> GetUserSubscription()
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithSubscription(User);

            return _mapper.Map<SubscriptionType, SubscriptionDto>(user.SubscriptionType);
        }
    }
}