using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Interface;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var result = _authService.RefreshToken(dto);

            return Ok(
                ApiResponse<LoginResponseDto>
                    .SuccessResponse("Token refreshed successfully", result)
            );
        }
    }
}
