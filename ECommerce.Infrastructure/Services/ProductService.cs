using ECommerce.Application.DTO.Common;
using ECommerce.Application.DTO.Product;
using ECommerce.Application.Exceptions;
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

        public async Task<PaginatedResponseDto<ProductResponseDto>>
     GetAllProductsAsync(PaginationRequestDto pagination)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = products.Select(p => new ProductResponseDto
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

            return new PaginatedResponseDto<ProductResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == dto.CategoryId);

            if (category == null)
                throw new Exception("Category not found");

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

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = category.Name
            };
        }
        public async Task<ProductResponseDto> GetProductByIdAsync(Guid productId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new NotFoundException("Product not found");

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name
            };
        }
        public async Task<PaginatedResponseDto<ProductResponseDto>>
    GetProductsByCategoryAsync(Guid categoryId, PaginationRequestDto pagination)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == categoryId);

            if (!categoryExists)
                throw new NotFoundException("Category not found");

            var query = _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .AsNoTracking()
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            var items = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            }).ToList();

            return new PaginatedResponseDto<ProductResponseDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }




    }
}
