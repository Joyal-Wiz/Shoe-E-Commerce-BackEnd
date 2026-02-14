using ECommerce.Application.DTO.Product;
using ECommerce.Application.Interface;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()  //Tracking is unnecessary overhead.
                .ToListAsync();  //to execute a database query and retrieve results as a List<T>

            var result = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null
            }).ToList();


            return result;
        }
        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            // Check if category exists
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == dto.CategoryId);

            if (!categoryExists)
                throw new Exception("Category not found");

            // Create product entity
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            // Save to database
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Return response DTO
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId
            };
        }

    }
}
