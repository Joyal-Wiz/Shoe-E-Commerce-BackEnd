using ECommerce.Application.DTO.Admin;
using ECommerce.Application.DTO.Common;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerce.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedResponseDto<UserResponseDto>>
            GetAllUsersAsync(PaginationRequestDto pagination)
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

    }
}