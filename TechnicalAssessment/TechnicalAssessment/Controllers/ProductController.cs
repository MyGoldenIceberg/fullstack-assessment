using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalAssessment.Core.DTOs;
using TechnicalAssessment.Core.EnumHelper;
using TechnicalAssessment.Core.Helper;
using TechnicalAssessment.Core.Model;
using TechnicalAssessment.Core.Services.Interfaces;

namespace TechnicalAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILogger<ProductController> logger) : base(logger)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return ApiResponse(products);
        }


        [HttpPost]

        [ProducesResponseType(typeof(ApiResponse<Product>), 200)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createDto)
        {
            try
            {
                var product = await _productService.CreateProductAsync(createDto);
                return ApiResponse(product, codes: ApiResponseCodes.OK);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation failed.");
                return ApiResponse<string>(errors: ex.Message, codes: ApiResponseCodes.INVALID_REQUEST);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                return ApiResponse<string>(errors: "An unexpected error occurred.", codes: ApiResponseCodes.EXCEPTION);
            }
        }
    }
}
