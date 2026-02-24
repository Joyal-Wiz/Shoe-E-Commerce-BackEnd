using ECommerce.Application.DTO.Auth;
using ECommerce.Application.Interface;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Resources;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            var response = ApiResponse<LoginResponseDto>
                .SuccessResponse(SuccessMessages.Loginsuccessful, result, 200);

            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshTokenDto dto)
        {
            var result = _authService.RefreshToken(dto);

            if (result == null)
            {
                var failure = ApiResponse<LoginResponseDto>
                    .FailureResponse(ErrorMessages.Invalidrefreshtoken, 401);

                return StatusCode(failure.StatusCode, failure);
            }

            var success = ApiResponse<LoginResponseDto>
                .SuccessResponse(SuccessMessages.Tokenrefreshedsuccessfully, result, 200);

            return StatusCode(success.StatusCode, success);
        }
    }
}