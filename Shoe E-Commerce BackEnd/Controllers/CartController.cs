using ECommerce.Application.DTO.Cart;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(Roles = "User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _cartService
                .GetCartAsync(Guid.Parse(userId));

            return Ok(
                ApiResponse<List<CartItemResponseDto>>
                    .SuccessResponse(SuccessMessages.CartFetchedSuccessfully, result)
            );
        }

        [Authorize(Roles = "User")]
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToCart(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _cartService
                .AddToCartAsync(Guid.Parse(userId), productId);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(SuccessMessages.CartItemAddedSuccessfully, null)
            );
        }

        [Authorize(Roles = "User")]
        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateCartItem(
            Guid itemId,
            UpdateCartItemDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _cartService.UpdateCartItemAsync(
                Guid.Parse(userId),
                itemId,
                dto.Quantity);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(SuccessMessages.CartItemUpdatedSuccessfully, null)
            );
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteCartItem(Guid itemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _cartService.DeleteCartItemAsync(
                Guid.Parse(userId),
                itemId);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(SuccessMessages.CartItemDeletedSuccessfully, null)
            );
        }
    }
}
