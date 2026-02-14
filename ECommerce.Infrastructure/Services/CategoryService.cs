using ECommerce.Application.DTO.Category;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Interface;
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
            // 1️⃣ Check if category already exists
            var exists = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower());

            if (exists)
                throw new AlreadyExistsException("Category already exists");

            // 2️⃣ Create entity
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            // 3️⃣ Save
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // 4️⃣ Return DTO
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
