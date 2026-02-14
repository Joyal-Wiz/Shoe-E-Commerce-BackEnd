using ECommerce.Application.DTO.Common;
using ECommerce.Application.DTO.Product;
using System;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface IProductService
    {
        // Get All (Paginated)
        Task<PaginatedResponseDto<ProductResponseDto>>
            GetAllProductsAsync(PaginationRequestDto pagination);

        // Get By Category (Paginated)
        Task<PaginatedResponseDto<ProductResponseDto>>
            GetProductsByCategoryAsync(Guid categoryId, PaginationRequestDto pagination);

        // Get By Id
        Task<ProductResponseDto>
            GetProductByIdAsync(Guid productId);

        // Create
        Task<ProductResponseDto>
            CreateProductAsync(CreateProductDto dto);
    }
}
