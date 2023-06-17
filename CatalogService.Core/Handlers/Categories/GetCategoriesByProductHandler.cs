using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogService.Core.Handlers.Categories;

public class GetCategoriesByProductHandler : IGetCategoriesByProductHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;

    public GetCategoriesByProductHandler(ICategoryRepository categoryRepository, IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Category>> Handle(GetCategoriesByProductIdQuery request, CancellationToken cancellationToken) =>
        await _memoryCache.GetOrCreateAsync($"product_{request.ProductId}_categories", async entry =>
        {
            IEnumerable<CategoryDto> categoriesDto =
                await _categoryRepository.GetCategoriesByProductAsync(request.ProductId);
            IEnumerable<Category> categories = categoriesDto.Select(CommonMapper.MapToCategory);
            entry.Value = categories;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return categories;
        });
}