using ECommerce.Application.DTO.Category;
using ECommerce.Application.DTO.Common;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            // Check if category already exists
            var exists = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower());

            if (exists)
                throw new AlreadyExistsException(ErrorMessages.CategoryAlreadyExists);

            // Create entity
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            // Save
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Return DTO
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<PaginatedResponseDto<CategoryResponseDto>>
    GetAllCategoriesAsync(PaginationRequestDto pagination)
        {
            var query = _context.Categories
                .AsNoTracking()
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var categories = await query
                .OrderBy(c => c.Name)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .Select(c => new CategoryResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();

            return new PaginatedResponseDto<CategoryResponseDto>
            {
                Items = categories,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pagination.PageSize)
            };
        }

        public async Task<CategoryResponseDto> UpdateCategoryAsync(
            Guid categoryId,
            UpdateCategoryDto dto)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new NotFoundException(ErrorMessages.CategoryNotFound);

            var duplicateExists = await _context.Categories
                .AnyAsync(c =>
                    c.Id != categoryId &&
                    c.Name.ToLower().Trim() ==
                    dto.Name.ToLower().Trim());

            if (duplicateExists)
                throw new AlreadyExistsException(
                    ErrorMessages.CategoryAlreadyExists);

            category.Name = dto.Name.Trim();
            category.Description = dto.Description?.Trim();

            await _context.SaveChangesAsync();

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
                throw new NotFoundException(ErrorMessages.CategoryNotFound);

            var hasProducts = await _context.Products
                .AnyAsync(p => p.CategoryId == categoryId && !p.IsDeleted);

            if (hasProducts)
                throw new BadRequestException(ErrorMessages.categoryassigned);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
