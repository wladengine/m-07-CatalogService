using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Product;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogService.Core.Handlers.Products;

public class GetProductsHandler : IGetProductsHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _memoryCache;

    public GetProductsHandler(IProductRepository productRepository, IMemoryCache memoryCache)
    {
        _productRepository = productRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Product[]> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) =>
        await _memoryCache.GetOrCreateAsync("productsAll", async entry =>
        {
            IEnumerable<ProductDto> productsDto = await _productRepository.GetProductsAsync();
            Product[] products = productsDto.Select(CommonMapper.MapToProduct).ToArray();
            entry.Value = products;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return products;
        });
}