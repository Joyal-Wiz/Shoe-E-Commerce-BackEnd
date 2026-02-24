using ECommerce.Application.DTO.Category;
using ECommerce.Application.DTO.Common;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(
           [FromQuery] PaginationRequestDto pagination)
        {
            var result = await _categoryService
                .GetAllCategoriesAsync(pagination);

            return Ok(
                ApiResponse<PaginatedResponseDto<CategoryResponseDto>>
                    .SuccessResponse(
                        SuccessMessages.Categoryfetchedsuccessfully,
                        result
                    )
            );
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            var result = await _categoryService.CreateCategoryAsync(dto);
            return StatusCode(201,ApiResponse<CategoryResponseDto>
        .SuccessResponse(SuccessMessages.CategoryCreatedSuccessfully,result,201));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(
            Guid id,
            [FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryService
                .UpdateCategoryAsync(id, dto);

            var response = ApiResponse<CategoryResponseDto>
                .SuccessResponse(
                    SuccessMessages.CategoryUpdatedSuccessfully,
                    result
                );

            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            var response = ApiResponse<object>
                .SuccessResponse(SuccessMessages.CategoryDeletedSuccessfully,null);

            return StatusCode(response.StatusCode, response);
        }
    }
}
