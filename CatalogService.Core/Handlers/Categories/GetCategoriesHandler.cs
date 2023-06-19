using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogService.Core.Handlers.Categories;

public class GetCategoriesHandler : IGetCategoriesHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;

    public GetCategoriesHandler(ICategoryRepository categoryRepository, IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Category[]> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken) =>
        await _memoryCache.GetOrCreateAsync("categoriesAll", async entry =>
        {
            IEnumerable<CategoryDto> categoriesDto = await _categoryRepository.GetCategoriesAsync();
            Category[] categories = categoriesDto.Select(CommonMapper.MapToCategory).ToArray();
            entry.Value = categories;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return categories;
        });
}