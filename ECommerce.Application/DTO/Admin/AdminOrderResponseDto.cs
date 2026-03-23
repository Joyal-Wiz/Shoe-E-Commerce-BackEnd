namespace ECommerce.Application.DTO.Admin
{
    public class AdminOrderResponseDto
    {
        public Guid OrderId { get; set; }

        public string CustomerEmail { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}