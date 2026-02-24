using ECommerce.Application.DTO.Category;
using ECommerce.Application.DTO.Common;
using System.Threading.Tasks;

namespace ECommerce.Application.Interface
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task<PaginatedResponseDto<CategoryResponseDto>> GetAllCategoriesAsync(PaginationRequestDto pagination);
        Task<CategoryResponseDto> UpdateCategoryAsync(Guid categoryId,UpdateCategoryDto dto);

        Task DeleteCategoryAsync(Guid categoryId);

    }
}
