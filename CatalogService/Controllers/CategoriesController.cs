using CatalogService.Core.Commands;
using CatalogService.Core.Commands.Category;
using CatalogService.Core.Entities;
using CatalogService.Core.Handlers.Categories;
using CatalogService.Core.Handlers.Products;
using CatalogService.Core.Queries.Category;
using CatalogService.Core.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMediator _mediator;

        public CategoriesController(ILogger<CategoriesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET /api/categories
        [HttpGet(Name = "Get all categories")]
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync() => 
            await _mediator.Send(new GetAllCategoriesQuery());

        // GET /api/categories/123
        [HttpGet(template: "{id}", Name = "Get single category")]
        public async Task<Category> GetCategoryByIdAsync(int id) => 
            await _mediator.Send(new GetCategoryByIdQuery(id));

        // GET /api/categories/123/products
        [HttpGet(template: "{id}/products", Name = "Get products by category")]
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int id) => 
            await _mediator.Send(new GetProductsByCategoryIdQuery(id));

        // POST /api/categories
        [HttpPost(Name = "Add new category")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            Category category = await _mediator.Send(createCategoryCommand);
            return Created($"api/categories/{category.Id}", category);
        }

        // PUT /api/categories/123
        [HttpPut("{id}", Name = "UpdateAsync existing category")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateCategoryCommand createCategoryCommand)
        {
            Category? updatedCategory = await _mediator.Send(
                new UpdateCategoryCommand(id, createCategoryCommand.Name, createCategoryCommand.Description));
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        // DELETE /api/categories/123
        [HttpDelete("{id}", Name = "DeleteAsync existing category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool categoryExists = await _mediator.Send(new DeleteCategoryCommand(id));
            return categoryExists ? NoContent() : NotFound();
        }
    }
}
