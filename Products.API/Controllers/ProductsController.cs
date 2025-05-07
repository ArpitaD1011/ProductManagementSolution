using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTOs;
using Products.Application.Services;
using Products.Domain.Entities;

namespace Products.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
            private readonly IProductService _productService;
            public ProductsController(IProductService productService)
            {
                _productService = productService;
            }
            //GET: api/products
            [HttpGet]
            public async Task<IActionResult> GetAllProducts()
            {
                var products = await _productService.GetAllProductAsync();
                var response = products.Select(p => new GetProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    StockAvailable = p.StockAvailable
                });
                return Ok(response);
            }
            //GET: api/products/{id}
            [HttpGet("{id}")]
            public async Task<IActionResult> GetProductById(string id)
            {
                var product = await _productService.GetProductByIdAsync(id);
                if(product == null) return NotFound();
                var response = new GetProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    StockAvailable = product.StockAvailable
                };
                return Ok(response);
            }
            //POST: api/products
            [HttpPost]
            public async Task<IActionResult> AddNewProduct([FromBody] AddProductDto addProductDto)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var product = new Product
                {
                ProductName = addProductDto.ProductName,
                ProductDescription = addProductDto.ProductDescription,
                StockAvailable = addProductDto.StockAvailable
                };
                var created = await _productService.AddProductAsync(product);
            var response = new GetProductDto
            {
                ProductId = created.ProductId,
                ProductName = created.ProductName,
                ProductDescription = created.ProductDescription,
                StockAvailable = created.StockAvailable
            };
                return CreatedAtAction(nameof(GetProductById), new { id = response.ProductId }, response);

            }
            //PUT: api/products/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDto updateProductDto)
            {
                var existing = await _productService.GetProductByIdAsync(id);
                if (existing == null) return NotFound();
                existing.ProductName = updateProductDto.ProductName;
                existing.ProductDescription = updateProductDto.ProductDescription;
                existing.StockAvailable = updateProductDto.StockAvailable;
                await _productService.UpdateProductAsync(id, existing);
            var response = new UpdateProductDto
            {
                ProductId = existing.ProductId,
                ProductName = existing.ProductName,
                ProductDescription = existing.ProductDescription,
                StockAvailable = existing.StockAvailable
            };
                return Ok(response);
            }
            //DELETE: api/products/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteProduct(string id)
            {
            var existing = await _productService.GetProductByIdAsync(id);
            if (existing == null) return NotFound();
            await _productService.DeleteProductAsync(id);
                return Ok();
            }

            [HttpPut("decreament-stock/{id}/{quantity}")]
            public async Task<IActionResult> DecreamentProductStock(string id, int quantity)
            {
                await _productService.DecrementStockAsync(id, quantity);
                return Ok();

            }
            [HttpPut("add-to-stock/{id}/{quantity}")]
            public async Task<IActionResult> AddProductStock(string id, int quantity)
            {
                await _productService.AddToStockAsync(id, quantity);
                return Ok();

            }
    }
}
