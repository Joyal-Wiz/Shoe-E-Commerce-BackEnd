using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interface
{
    public interface IOrderRepository
    {
        Task<(List<Order> Orders, int TotalCount)> GetPagedOrdersAsync(
            int pageNumber,
            int pageSize);
        Task<decimal> GetTotalRevenueAsync();
    }
}