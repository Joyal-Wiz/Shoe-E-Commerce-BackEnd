using ECommerce.Application.DTO.Wishlist;
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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [Authorize(Roles = "User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetWishlist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _wishlistService
                .GetWishlistAsync(Guid.Parse(userId));

            return Ok(
                ApiResponse<List<WishlistItemResponseDto>>
                    .SuccessResponse(
                        SuccessMessages.WishlistFetchedSuccessfully,
                        result
                    )
            );
        }

        [Authorize(Roles = "User")]
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToWishlist(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _wishlistService
                .AddToWishlistAsync(Guid.Parse(userId), productId);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        SuccessMessages.WishlistItemAddedSuccessfully,
                        null
                    )
            );
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteWishlistItem(Guid itemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _wishlistService.DeleteWishlistItemAsync(
                Guid.Parse(userId),
                itemId);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(
                        SuccessMessages.WishlistItemDeletedSuccessfully,
                        null
                    )
            );
        }
    }
}
