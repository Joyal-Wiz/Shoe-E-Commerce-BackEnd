using ECommerce.Application.DTO.Admin;
using ECommerce.Application.DTO.Common;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;


namespace ECommerce.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public AdminService(
            IUserRepository userRepository,
            IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        // ============================
        // GET ALL USERS (PAGINATION)
        // ============================
        public async Task<PaginatedResponseDto<UserResponseDto>> GetAllUsersAsync(
            PaginationRequestDto pagination)
        {
            var (users, totalCount) =
                await _userRepository.GetPagedUsersAsync(
                    pagination.PageNumber,
                    pagination.PageSize);

            var items = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role.ToString(),
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            }).ToList();

            return new PaginatedResponseDto<UserResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pagination.PageSize)
            };
        }

        // ============================
        // GET USER BY ID
        // ============================
        public async Task<UserResponseDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new NotFoundException(ErrorMessages.Usernotfound);

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString(),
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        // ============================
        // BLOCK USER
        // ============================
        public async Task<UserStatusResponseDto> BlockUserAsync(
            Guid userId,
            Guid currentAdminId)
        {
            // Prevent admin blocking themselves
            if (userId == currentAdminId)
                throw new BadRequestException(
                    ErrorMessages.Admincannotblocktheiraccount);

            var user = await _userRepository.GetByIdTrackingAsync(userId);

            if (user == null)
                throw new NotFoundException(ErrorMessages.Usernotfound);

            // Prevent blocking already blocked user
            if (!user.IsActive)
                throw new BadRequestException(
                    ErrorMessages.Userisalreadyblocked);

            user.IsActive = false;

            await _userRepository.SaveChangesAsync();

            return new UserStatusResponseDto
            {
                Id = user.Id,
                IsActive = user.IsActive
            };
        }

        // ============================
        // UNBLOCK USER
        // ============================
        public async Task<UserStatusResponseDto> UnblockUserAsync(
            Guid userId,
            Guid currentAdminId)
        {
            // Prevent admin modifying themselves
            if (userId == currentAdminId)
                throw new BadRequestException(
                    ErrorMessages.Admincannotmodifytheiraccount);

            var user = await _userRepository.GetByIdTrackingAsync(userId);

            if (user == null)
                throw new NotFoundException(ErrorMessages.Usernotfound);

            // Prevent unblocking already active user
            if (user.IsActive)
                throw new BadRequestException(
                    ErrorMessages.Userisalreadyactive);

            user.IsActive = true;

            await _userRepository.SaveChangesAsync();

            return new UserStatusResponseDto
            {
                Id = user.Id,
                IsActive = user.IsActive
            };
        }

        // ============================
        // GET ALL ORDERS (ADMIN VIEW)
        // ============================
        public async Task<PaginatedResponseDto<AdminOrderResponseDto>> GetAllOrdersAsync(
     PaginationRequestDto pagination)
        {
            var (orders, totalCount) =
                await _orderRepository.GetPagedOrdersAsync(
                    pagination.PageNumber,
                    pagination.PageSize);

            var items = orders.Select(o => new AdminOrderResponseDto
            {
                OrderId = o.Id,
                CustomerEmail = o.User.Email,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt
            }).ToList();

            return new PaginatedResponseDto<AdminOrderResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pagination.PageSize)
            };
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _orderRepository.GetTotalRevenueAsync();
        }
    }
    }