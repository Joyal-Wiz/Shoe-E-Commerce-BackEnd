using ECommerce.Application.DTO.Common;
using ECommerce.Application.DTO.Product;
using System;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface IProductService
    {
        Task<PaginatedResponseDto<ProductResponseDto>>
            GetAllProductsAsync(PaginationRequestDto pagination);

        
        Task<PaginatedResponseDto<ProductResponseDto>>
            GetProductsByCategoryAsync(Guid categoryId, PaginationRequestDto pagination);

        Task<ProductResponseDto>
            GetProductByIdAsync(Guid productId);

        
        Task<ProductResponseDto>
            CreateProductAsync(CreateProductDto dto);
        Task<PaginatedResponseDto<ProductResponseDto>>
    SearchProductsAsync(string query, PaginationRequestDto pagination);

    }
}
