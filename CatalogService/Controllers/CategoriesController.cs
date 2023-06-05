using CatalogService.Core.Commands;
using CatalogService.Core.Entities;
using CatalogService.Core.Handlers.Categories;
using CatalogService.Core.Handlers.Products;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IGetCategoriesHandler _getCategoriesHandler;
        private readonly IGetCategoryHandler _getCategoryHandler;
        private readonly ICreateCategoryHandler _createCategoryHandler;
        private readonly IUpdateCategoryHandler _updateCategoryHandler;
        private readonly IDeleteCategoryHandler _deleteCategoryHandler;
        private readonly IGetProductsByCategoryHandler _getProductsByCategoryHandler;

        public CategoriesController(
            ILogger<CategoriesController> logger,
            IGetCategoriesHandler getCategoriesHandler,
            IGetCategoryHandler getCategoryHandler,
            ICreateCategoryHandler createCategoryHandler,
            IUpdateCategoryHandler updateCategoryHandler,
            IDeleteCategoryHandler deleteCategoryHandler,
            IGetProductsByCategoryHandler getProductsByCategoryHandler)
        {
            _logger = logger;
            _getCategoriesHandler = getCategoriesHandler;
            _getCategoryHandler = getCategoryHandler;
            _createCategoryHandler = createCategoryHandler;
            _updateCategoryHandler = updateCategoryHandler;
            _deleteCategoryHandler = deleteCategoryHandler;
            _getProductsByCategoryHandler = getProductsByCategoryHandler;
        }


        // GET /api/categories
        [HttpGet(Name = "Get all categories")]
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _getCategoriesHandler.HandleAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(GetAllCategoriesAsync)}");
                throw;
            }
        }

        // GET /api/categories/123
        [HttpGet(template: "{id}", Name = "Get single category")]
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _getCategoryHandler.HandleAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(GetCategoryByIdAsync)}");
                throw;
            }
        }

        // GET /api/categories/123/products
        [HttpGet(template: "{id}/products", Name = "Get products by category")]
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int id)
        {
            try
            {
                return await _getProductsByCategoryHandler.HandleAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(GetProductsByCategoryIdAsync)}");
                throw;
            }
        }

        // POST /api/categories
        [HttpPost(Name = "Add new category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCommand categoryCommand)
        {
            try
            {
                int id = await _createCategoryHandler.HandleAsync(categoryCommand);
                return Created($"api/categories/{id}", categoryCommand);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(AddCategory)}");
                throw;
            }
        }

        // PUT /api/categories/123
        [HttpPut("{id}", Name = "Update existing category")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCommand categoryCommand)
        {
            try
            {
                Category? updatedCategory = await _updateCategoryHandler.HandleAsync(id, categoryCommand);
                if (updatedCategory == null)
                {
                    return NotFound();
                }

                return Ok(updatedCategory);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(UpdateCategory)}");
                throw;
            }
        }

        // DELETE /api/categories/123
        [HttpDelete("{id}", Name = "Delete existing category")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                bool categoryExists = await _deleteCategoryHandler.HandleAsync(id);
                return categoryExists ? Ok() : NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while {nameof(DeleteCategory)}");
                throw;
            }
        }
    }
}
