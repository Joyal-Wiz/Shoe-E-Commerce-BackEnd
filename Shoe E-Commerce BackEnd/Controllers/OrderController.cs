using ECommerce.Application.DTO.Order;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _orderService
                .CreateOrderAsync(Guid.Parse(userId));

            return Ok(
                ApiResponse<OrderResponseDto>
                    .SuccessResponse(SuccessMessages.OrderCreatedSuccessfully, result)
            );
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _orderService
                .GetUserOrdersAsync(Guid.Parse(userId));

            return Ok(
                ApiResponse<List<OrderResponseDto>>
                    .SuccessResponse(SuccessMessages.OrdersFetchedSuccessfully, result)
            );
        }

        [Authorize(Roles = "User")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var result = await _orderService
                .GetOrderByIdAsync(Guid.Parse(userId), orderId);

            return Ok(
                ApiResponse<OrderResponseDto>
                    .SuccessResponse(SuccessMessages.OrderFetchedSuccessfully, result)
            );
        }

        [Authorize(Roles = "User")]
        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            await _orderService
                .CancelOrderAsync(Guid.Parse(userId), orderId);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(SuccessMessages.OrderCancelledSuccessfully, null)
            );
        }
    }
}
