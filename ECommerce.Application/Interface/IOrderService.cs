using ECommerce.Application.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{

    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(Guid userId);
        Task<List<OrderResponseDto>> GetUserOrdersAsync(Guid userId);
        Task<OrderResponseDto> GetOrderByIdAsync(Guid userId, Guid orderId);
        Task<string> CancelOrderAsync(Guid userId, Guid orderId);


    }
}
