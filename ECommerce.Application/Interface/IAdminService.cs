using ECommerce.Application.DTO.Admin;
using ECommerce.Application.DTO.Common;

namespace ECommerce.Application.Interface
{
    public interface IAdminService
    {
        Task<PaginatedResponseDto<UserResponseDto>>
            GetAllUsersAsync(PaginationRequestDto pagination);

        Task<UserResponseDto> GetUserByIdAsync(Guid userId);
        Task<PaginatedResponseDto<AdminOrderResponseDto>>
    GetAllOrdersAsync(PaginationRequestDto pagination);
        Task<decimal> GetTotalRevenueAsync();

        Task<UserStatusResponseDto> BlockUserAsync(
            Guid userId,
            Guid currentAdminId);
        Task<UserStatusResponseDto> UnblockUserAsync(
    Guid userId,
    Guid currentAdminId);
    }
}