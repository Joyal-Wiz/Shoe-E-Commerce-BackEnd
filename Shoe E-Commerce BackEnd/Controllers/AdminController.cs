using ECommerce.Application.Constants;
using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Interface;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Shoe_E_Commerce_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginDto dto)
        {
            var response = _authService.AdminLogin(dto);

            return Ok(ApiResponse<LoginResponseDto>
                .SuccessResponse(ApiMessages.Success.AdminLogin, response));
        }
    }
}
