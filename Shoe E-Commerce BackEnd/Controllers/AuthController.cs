using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Shoe_E_Commerce_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshTokenDto dto)
        {
            var response = _authService.RefreshToken(dto);
            return Ok(response);
        }
    }
}
