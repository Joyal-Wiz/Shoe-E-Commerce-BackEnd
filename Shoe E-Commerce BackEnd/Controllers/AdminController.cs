using ECommerce.Application.DTO.Admin;
using ECommerce.Application.DTO.Common;
using ECommerce.Application.DTO.Product;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Application.Responses;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IProductService _productService;

        public AdminController(IAdminService adminService, IProductService productService)
        {
            _adminService = adminService;
            _productService = productService;

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
        [HttpPut("users/{id}/block")]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            var currentAdminId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            await _adminService.BlockUserAsync(id, currentAdminId);

            return Ok(
                ApiResponse<string>.SuccessResponse(
                    SuccessMessages.UserBlockedSuccessfully,null
                )
            );
        }

        [HttpPut("users/{id}/unblock")]
        public async Task<IActionResult> UnblockUser(Guid id)
        {
            var currentAdminId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var result = await _adminService.UnblockUserAsync(id, currentAdminId);

            return Ok(
                ApiResponse<UserStatusResponseDto>
                    .SuccessResponse(
                        SuccessMessages.UserUnblockedSuccessfully,
                        result
                    )
            );
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct(
    [FromBody] CreateProductDto dto)
        {
            var result = await _productService.CreateProductAsync(dto);

            var response = ApiResponse<ProductResponseDto>
                .SuccessResponse(
                    SuccessMessages.ProductCreatedSuccessfully,
                    result,
                    201
                );

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("productUpdate/{id}")]
        public async Task<IActionResult> UpdateProduct(
            Guid id,
            [FromBody] UpdateProductDto dto)
        {
            var result = await _productService.UpdateProductAsync(id, dto);

            var response = ApiResponse<ProductResponseDto>
                .SuccessResponse(
                    SuccessMessages.ProductUpdatedSuccessfully,
                    result
                );

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);

            var response = ApiResponse<object>
                .SuccessResponse(
                    SuccessMessages.ProductDeletedSuccessfully,
                    null
                );

            return StatusCode(response.StatusCode, response);
        }
    }
}