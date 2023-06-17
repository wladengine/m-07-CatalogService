using CatalogService.Core.Commands.Product;
using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Core.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(
            ILogger<ProductsController> logger, 
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET /api/products
        [HttpGet(Name = "Get all products")]
        public async Task<Product[]> GetAllProductsAsync() => 
            await _mediator.Send(new GetAllProductsQuery());

        // GET /api/products/123
        [HttpGet(template: "{id}", Name = "Get single createProduct")]
        public async Task<Product> GetProductByIdAsync(int id) => 
            await _mediator.Send(new GetProductByIdQuery(id));

        // GET /api/products/123/categories
        [HttpGet(template: "{id}/categories", Name = "Get createProduct categories")]
        public async Task<Category[]> GetCategoriesByProductIdAsync(int id) => 
            await _mediator.Send(new GetCategoriesByProductIdQuery(id));

        // POST /api/products
        [HttpPost(Name = "Add new createProduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            var request = new CreateProductCommand(
                command.Name,
                command.Description,
                command.Price,
                command.CategoryIds);
            Product product = await _mediator.Send(request);
            return Created($"api/products/{product.Id}", product);
        }

        // PUT /api/products/123
        [HttpPut("{id}", Name = "UpdateAsync existing createProduct")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] CreateProductCommand command)
        {
            var request = new UpdateProductCommand(
                id,
                command.Name,
                command.Description,
                command.Price,
                command.CategoryIds);
            Product updatedProduct = await _mediator.Send(request);
            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

        // DELETE /api/products/123
        [HttpDelete("{id}", Name = "DeleteAsync existing createProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}