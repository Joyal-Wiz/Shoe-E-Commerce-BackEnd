using System;
using System.Collections.Generic;

namespace ECommerce.Application.DTO.Order
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItemResponseDto> Items { get; set; }
    }
}
