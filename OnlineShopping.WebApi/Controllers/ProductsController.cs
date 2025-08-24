using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineShopping.Business.Operations.Product;
using OnlineShopping.Business.Operations.Product.Dtos;
using OnlineShopping.WebApi.Filters;
using OnlineShopping.WebApi.Models;

namespace OnlineShopping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {
            var addProductDto = new AddProductDto
            {
                ProductName = request.ProductName,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            var result = await _productService.AddProduct(addProductDto);

            if (result.IsSucceed)
                return Ok();
            else
                return BadRequest(result.Message);

        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product is null)
                return NotFound();
            else
                return Ok(product);
        }

        [HttpPatch("{id}/price")]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> AdjustProductPrice(int id, decimal changeTo)
        {
            var result = await _productService.AdjustProductPrice(id, changeTo);

            if (!result.IsSucceed)
                return NotFound();
            else
                return Ok();
        }

        [HttpPatch("{id}/stockQuantity")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdjustProductStockQuantity(int id, int changeTo)
        {
            var result = await _productService.AdjustProductStockQuantity(id, changeTo);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProduct(id);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request)
        {
            var updateProductDto = new UpdateProductDto
            {
                Id = id,
                ProductName = request.ProductName,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            var result = await _productService.UpdateProduct(updateProductDto);

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();

        }
    }
}
