using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public interface IGetCategoriesByProductHandler
{
    Task<IEnumerable<Category>> HandleAsync(int productId);
}

public class GetCategoriesByProductHandler : IGetCategoriesByProductHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesByProductHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Category>> HandleAsync(int productId)
    {
        IEnumerable<CategoryDto> groups = await _categoryRepository.GetCategoriesByProduct(productId);
        return groups.Select(CommonMapper.MapToCategory).ToArray();
    }
}