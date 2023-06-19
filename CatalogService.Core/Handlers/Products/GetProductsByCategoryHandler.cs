using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Product;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogService.Core.Handlers.Products;

public class GetProductsByCategoryHandler : IGetProductsByCategoryHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _memoryCache;

    public GetProductsByCategoryHandler(IProductRepository productRepository, IMemoryCache memoryCache)
    {
        _productRepository = productRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Product[]> Handle(GetProductsByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var key = $"category_{request.CategoryId}_products";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            IEnumerable<ProductDto> productsDto = await _productRepository.GetProductsByCategory(request.CategoryId);
            Product[] products = productsDto.Select(CommonMapper.MapToProduct).ToArray();
            entry.Value = products;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return products;
        });
    }
}