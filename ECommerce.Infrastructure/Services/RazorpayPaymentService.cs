using ECommerce.Application.Interface;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Infrastructure.Services
{
    public class RazorpayPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public RazorpayPaymentService(
            IConfiguration configuration,
            AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> CreateOrderForExistingOrderAsync(Guid orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                throw new Exception("Order not found.");

            if (order.Status != OrderStatus.Pending)
                throw new Exception("Order is not in Pending state.");

            var key = _configuration["Razorpay:Key"];
            var secret = _configuration["Razorpay:Secret"];

            var client = new RazorpayClient(key, secret);

            var options = new Dictionary<string, object>
            {
                { "amount", order.TotalAmount * 100 }, 
                { "currency", "INR" },
                { "payment_capture", 1 }
            };

            Order razorpayOrder = client.Order.Create(options);

            order.RazorpayOrderId = razorpayOrder["id"].ToString();

            await _context.SaveChangesAsync();

            return order.RazorpayOrderId!;
        }

        public async Task VerifyAndUpdatePaymentAsync(
            string razorpayOrderId,
            string paymentId,
            string signature)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.RazorpayOrderId == razorpayOrderId);

            if (order == null)
                throw new Exception("Order not found.");

            var secret = _configuration["Razorpay:Secret"];
            var payload = $"{razorpayOrderId}|{paymentId}";

            using var hmac = new HMACSHA256(
                Encoding.UTF8.GetBytes(secret!)
            );

            var hash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(payload)
            );

            var generatedSignature = Convert.ToBase64String(hash);

            //if (generatedSignature != signature)
            //    throw new Exception("Payment verification failed.");

            order.Status = OrderStatus.Confirmed;
            order.RazorpayPaymentId = paymentId;
            order.PaymentVerifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}