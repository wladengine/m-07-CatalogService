using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogService.Core.Handlers.Categories;

public class GetCategoryHandler : IGetCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;

    public GetCategoryHandler(ICategoryRepository categoryRepository, IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) =>
        await _memoryCache.GetOrCreateAsync($"category_{request.Id}", async entry =>
        {
            CategoryDto categoryDto = await _categoryRepository.GetCategoryAsync(request.Id);
            Category category = CommonMapper.MapToCategory(categoryDto);
            entry.Value = category;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return category;
        });
}