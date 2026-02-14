using ECommerce.Application.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto);
    }

}
