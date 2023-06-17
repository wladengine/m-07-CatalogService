using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public class GetCategoriesHandler : IGetCategoriesHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CategoryDto> categories = await _categoryRepository.GetCategoriesAsync();
        return categories.Select(CommonMapper.MapToCategory);
    }
}