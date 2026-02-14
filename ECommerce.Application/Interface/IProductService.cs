using ECommerce.Application.DTO.Common;
using ECommerce.Application.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface IProductService
    {
        Task<PaginatedResponseDto<ProductResponseDto>>
    GetAllProductsAsync(PaginationRequestDto pagination);

        Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto);
        Task<ProductResponseDto> GetProductByIdAsync(Guid productId);

    }

}
