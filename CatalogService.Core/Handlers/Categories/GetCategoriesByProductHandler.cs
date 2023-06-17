using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public class GetCategoriesByProductHandler : IGetCategoriesByProductHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesByProductHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Category>> Handle(GetCategoriesByProductIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CategoryDto> categoriesDto = await _categoryRepository.GetCategoriesByProductAsync(request.ProductId);
        return categoriesDto.Select(CommonMapper.MapToCategory);
    }
}