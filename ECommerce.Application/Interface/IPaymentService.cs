using System;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface IPaymentService
    {
        Task<string> CreateOrderForExistingOrderAsync(Guid orderId);

        Task VerifyAndUpdatePaymentAsync(
            string razorpayOrderId,
            string paymentId,
            string signature
        );
    }
}