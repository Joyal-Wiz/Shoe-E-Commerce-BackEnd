using ECommerce.Application.DTO.Category;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto);
    }
}
