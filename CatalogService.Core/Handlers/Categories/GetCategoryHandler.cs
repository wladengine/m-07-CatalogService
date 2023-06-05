using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public interface IGetCategoryHandler
{
    Task<Category> HandleAsync(int categoryId);
}

public class GetCategoryHandler : IGetCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository) =>
        _categoryRepository = categoryRepository;

    public async Task<Category> HandleAsync(int categoryId)
    {
        CategoryDto category = await _categoryRepository.GetCategoryAsync(categoryId);
        return CommonMapper.MapToCategory(category);
    }
}