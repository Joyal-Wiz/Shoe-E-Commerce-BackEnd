using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interface
{
    public interface IUserRepository
    {
        Task<(IEnumerable<User> Users, int TotalCount)>
            GetPagedUsersAsync(int pageNumber, int pageSize);
        
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByIdTrackingAsync(Guid userId);
        Task SaveChangesAsync();
    }
}