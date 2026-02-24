using ECommerce.Application.DTO.Admin;
using ECommerce.Application.DTO.Common;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers(
    [FromQuery] PaginationRequestDto pagination)
        {
            var result = await _adminService.GetAllUsersAsync(pagination);

            return Ok(
                ApiResponse<PaginatedResponseDto<UserResponseDto>>
                    .SuccessResponse(SuccessMessages.Usersretrievedsuccessfully, result)
            );
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _adminService.GetUserByIdAsync(id);

            return Ok(
                ApiResponse<UserResponseDto>
                    .SuccessResponse(SuccessMessages.Usersretrievedsuccessfully, result)
            );
        }
    }
}