using ECommerce.Application.DTO.Order;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(Guid userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            //  Load cart with items and products
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
                throw new BadRequestException("Cart is empty");

            // Create Order
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);

            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();

            // Create OrderItems & update stock
            foreach (var item in cart.CartItems)
            {
                if (item.Product.Stock < item.Quantity)
                    throw new BadRequestException(
                        $"Insufficient stock for product {item.Product.Name}");

                // Update stock
                item.Product.Stock -= item.Quantity;

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    Product = item.Product //keep product reference
                };

                totalAmount += item.Product.Price * item.Quantity;
                orderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;

            _context.OrderItems.AddRange(orderItems);

            // Clear cart
            _context.CartItems.RemoveRange(cart.CartItems);

            // Save & commit
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Response DTO (NO cart lookup)
            return new OrderResponseDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                Items = orderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };

        }
        public async Task<List<OrderResponseDto>> GetUserOrdersAsync(Guid userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return orders.Select(o => new OrderResponseDto
            {
                OrderId = o.Id,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt,
                Items = o.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            }).ToList();
        }

    }
}
