using ECommerce_API.Services.Abstract;
using Entities.DTO;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("add-product")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<ProductDTO>>> CreateProduct(ProductDTO productDTO)
        {
            var result = await _productService.AddProduct(productDTO);
            return Ok(result);
        }

        [HttpPost("delete-product/{Id}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ServiceResponse<int>>> DeleteProduct([FromRoute]int Id)
        {
            var result = await _productService.DeleteProduct(Id);
            return Ok(result);
        }
        [HttpPost("update-product")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<ServiceResponse<ProductDTO>>> UpdateProduct(ProductDTO productDTO)
        {
            var result = await _productService.UpdateProduct(productDTO);
            return Ok(result);
        }
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDTO>>>> GetAll()
        {
            var result = await _productService.GetAll();

            if (result == null)
            {
                return BadRequest("Ürün bulunmamaktadır");
            }

            return Ok(result);
        }
        [HttpGet("get-product")]
        public async Task<ActionResult<ServiceResponse<ProductDTO>>> GetProduct(string productName)
        {
            var result = await _productService.GetProduct(productName);
            return Ok(result);
        }

        [HttpGet("productByCategory/{categoryId}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByCategory([FromRoute]int categoryId)
        {
            var result = await _productService.GetProductsByCategory(categoryId);
            return Ok(result);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllByPagination(int page = 1)
        {
            var result = await _productService.GetProducts(page);
            return Ok(result);  
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductWithSearvhText([FromRoute]string searchText)
        {
            var result = await _productService.SearchProducts(searchText);
            return Ok(result);
        }

    }
}
