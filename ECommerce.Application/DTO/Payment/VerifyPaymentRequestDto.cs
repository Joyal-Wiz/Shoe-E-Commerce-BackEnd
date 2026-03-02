using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTO.Payment
{
    public class VerifyPaymentRequest
    {
        public string RazorpayOrderId { get; set; }
        public string PaymentId { get; set; }
        public string Signature { get; set; }
    }
}
