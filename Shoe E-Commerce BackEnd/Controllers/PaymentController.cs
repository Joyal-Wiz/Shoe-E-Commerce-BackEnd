using ECommerce.Application.DTO.Payment;
using ECommerce.Application.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create-order/{orderId}")]
    public async Task<IActionResult> CreateOrder(Guid orderId)
    {
        var razorpayOrderId =
            await _paymentService.CreateOrderForExistingOrderAsync(orderId);

        return Ok(new
        {
            RazorpayOrderId = razorpayOrderId
        });
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyPayment(
    [FromBody] VerifyPaymentRequest request)
    {
        await _paymentService.VerifyAndUpdatePaymentAsync(
            request.RazorpayOrderId,
            request.PaymentId,
            request.Signature
        );

        return Ok("Payment verified and order updated.");
    }
}