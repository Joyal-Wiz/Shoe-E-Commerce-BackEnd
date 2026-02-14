using ECommerce.Application.Constants;
using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Interface;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        // USER LOGIN
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _authService.Login(loginDto);

            return Ok(ApiResponse<LoginResponseDto>
                .SuccessResponse(ApiMessages.Success.Login, result));
        }

        // GET THE CURRENT USER
        [Authorize(Roles = "User")]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userData = new
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Username = User.Identity?.Name,
                Role = User.FindFirst(ClaimTypes.Role)?.Value
            };

            return Ok(ApiResponse<object>
                .SuccessResponse(ApiMessages.Success.UserDetailsFetched, userData));
        }

        // USER SIGNUP
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignUpDto dto)
        {
            await _authService.SignupAsync(dto);

            return Ok(ApiResponse<object>
                .SuccessResponse(ApiMessages.Success.Signup, null));
        }
    }
}
