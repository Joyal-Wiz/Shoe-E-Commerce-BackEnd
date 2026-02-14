using ECommerce.Application.DTO.Product;
using ECommerce.Application.Interface;
using ECommerce.Application.Resources;
using ECommerce.Application.Responses;
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

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(
                ApiResponse<List<ProductResponseDto>>
                    .SuccessResponse(
                        SuccessMessages.ProductsFetchedSuccessfully,
                        products
                    )
            );

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            var result = await _productService.CreateProductAsync(dto);
            return Ok(result);
        }

    }
}
