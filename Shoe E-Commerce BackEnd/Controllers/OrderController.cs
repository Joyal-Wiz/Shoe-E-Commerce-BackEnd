using ECommerce.Application.DTO.Order;
using ECommerce.Application.Interface;
using ECommerce.Application.Responses;
using ECommerce.Infrastructure.Services;
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
                    .SuccessResponse("Order created successfully", result)
            );
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var result = await _orderService.GetUserOrdersAsync(Guid.Parse(userId));

            return Ok(
                ApiResponse<List<OrderResponseDto>>
                    .SuccessResponse("Orders fetched successfully", result)
            );
        }
        [Authorize(Roles = "User")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var result = await _orderService
                .GetOrderByIdAsync(Guid.Parse(userId), orderId);

            return Ok(
                ApiResponse<OrderResponseDto>
                    .SuccessResponse("Order fetched successfully", result)
            );
        }
        [Authorize(Roles = "User")]
        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var result = await _orderService
                .CancelOrderAsync(Guid.Parse(userId), orderId);

            return Ok(
                ApiResponse<string>
                    .SuccessResponse(result, result)
            );
        }

    }
}
