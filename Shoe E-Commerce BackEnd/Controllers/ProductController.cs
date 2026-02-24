using ECommerce.Application.DTO.Common;
using ECommerce.Application.DTO.Product;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] PaginationRequestDto pagination)
        {
            var result = await _productService.GetAllProductsAsync(pagination);

            return Ok(
                ApiResponse<PaginatedResponseDto<ProductResponseDto>>
                    .SuccessResponse(
                        SuccessMessages.ProductsFetchedSuccessfully,
                        result
                    )
            );
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result = await _productService.GetProductByIdAsync(productId);

            return Ok(
                ApiResponse<ProductResponseDto>
                    .SuccessResponse(
                        SuccessMessages.ProductFetchedSuccessfully,
                        result
                    )
            );
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(
            Guid categoryId,
            [FromQuery] PaginationRequestDto pagination)
        {
            var result = await _productService
                .GetProductsByCategoryAsync(categoryId, pagination);

            return Ok(
                ApiResponse<PaginatedResponseDto<ProductResponseDto>>
                    .SuccessResponse(
                        SuccessMessages.ProductsFetchedSuccessfully,
                        result
                    )
            );
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(
            [FromQuery] string query,
            [FromQuery] PaginationRequestDto pagination)
        {
            var result = await _productService
                .SearchProductsAsync(query, pagination);

            return Ok(
                ApiResponse<PaginatedResponseDto<ProductResponseDto>>
                    .SuccessResponse(
                        SuccessMessages.ProductsFetchedSuccessfully,
                        result
                    )
            );
        }
    }
}
