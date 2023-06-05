using CatalogService.Core.Commands;
using CatalogService.Core.Entities;
using CatalogService.Core.Handlers.Categories;
using CatalogService.Core.Handlers.Products;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IGetProductsHandler _getProductsHandler;
        private readonly IGetProductHandler _getProductHandler;
        private readonly ICreateProductHandler _createProductHandler;
        private readonly IUpdateProductHandler _updateProductHandler;
        private readonly IDeleteProductHandler _deleteProductHandler;
        private readonly IGetCategoriesByProductHandler _getCategoriesByProductHandler;

        public ProductsController(
            ILogger<ProductsController> logger, 
            IGetProductsHandler getProductsHandler,
            IGetProductHandler getProductHandler,
            ICreateProductHandler createProductHandler,
            IUpdateProductHandler updateProductHandler,
            IDeleteProductHandler deleteProductHandler,
            IGetCategoriesByProductHandler getCategoriesByProductHandler)
        {
            _logger = logger;
            _getProductsHandler = getProductsHandler;
            _getProductHandler = getProductHandler;
            _createProductHandler = createProductHandler;
            _updateProductHandler = updateProductHandler;
            _deleteProductHandler = deleteProductHandler;
            _getCategoriesByProductHandler = getCategoriesByProductHandler;
        }

        // GET /api/products
        [HttpGet(Name = "Get all products")]
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _getProductsHandler.HandleAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(GetAllProductsAsync)}");
                throw;
            }
        }

        // GET /api/products/123
        [HttpGet(template: "{id}", Name = "Get single product")]
        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                return await _getProductHandler.HandleAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(GetProductByIdAsync)}");
                throw;
            }
        }
        
        // GET /api/products/123/categories
        [HttpGet(template: "{id}/categories", Name = "Get product categories")]
        public async Task<IEnumerable<Category>> GetCategoriesByProductIdAsync(int id)
        {
            try
            {
                return await _getCategoriesByProductHandler.HandleAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(GetCategoriesByProductIdAsync)}");
                throw;
            }
        }

        // POST /api/products
        [HttpPost(Name = "Add new product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductCommand productCommand)
        {
            try
            {
                int id = await _createProductHandler.HandleAsync(productCommand);
                return Created($"api/products/{id}", productCommand);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(AddProduct)}");
                throw;
            }
        }

        // PUT /api/products/123
        [HttpPut("{id}", Name = "Update existing product")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductCommand productCommand)
        {
            try
            {
                Product updatedProduct = await _updateProductHandler.HandleAsync(id, productCommand);
                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return Ok(updatedProduct);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(UpdateProduct)}");
                throw;
            }
        }

        // DELETE /api/products/123
        [HttpDelete("{id}", Name = "Delete existing product")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                bool productExists = await _deleteProductHandler.HandleAsync(id);
                return productExists ? Ok() : NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(DeleteProduct)}");
                throw;
            }
        }
    }
}