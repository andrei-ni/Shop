using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace DrPrint.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, DataContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet("admin/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductResponseDTO>>> GetAdminProducts(int page = 1)
        {
            var result = await _productService.GetAdminProducts(page);
            return Ok(result);
        }

        [HttpGet("newproducts")]
        public async Task<ActionResult<List<Product>>> GetNewProducts()
        {
            var result = await _productService.GetNewProducts();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateProducts(Product product)
        {
            var result = await _productService.CreateProduct(product);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProducts(Product product)
        {
            var result = await _productService.UpdateProduct(product);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProducts(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProduct()
        {
            var result = await _productService.GetProductAsync();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        // the [Route] is api/product/{productId}
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
        {
            var result = await _productService.GetProductAsync(productId);
            return Ok(result);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<List<Product>>> GetProductByCategoryAsync(string categoryUrl)
        {
            var result = await _productService.GetProductByCategoryAsync(categoryUrl);
            return Ok(result);
        }

        [HttpGet("subCategory/{subCategoryUrl}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductResponseDTO>>> GetProductBySubCategoryAsync(string subCategoryUrl, int page = 1)
        {
            var result = await _productService.GetProductBySubCategoryAsync(subCategoryUrl, page);
            return Ok(result);
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResultDTO>>> SearchProducts(string searchText, int page = 1)
        {
            var result = await _productService.SearchProducts(searchText, page);
            return Ok(result);
        }

        [HttpGet("searchsuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _productService.GetProductSearchSuggestions(searchText);
            return Ok(result);
        }
        [HttpGet("featured")]
        public async Task<ActionResult<ServiceResponse<ProductResponseDTO>>> GetFeaturedProducts()
        {
            var result = await _productService.GetFeaturedProducts();
            return Ok(result);
        }
        [HttpPut("view")]
        public async Task<ActionResult<ServiceResponse<Product>>> SetProductView(Product product)
        {
            var result = await _productService.SetProductView(product);
            return Ok(result);
        }
       
    }
}
