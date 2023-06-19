using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Product;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogService.Core.Handlers.Products;

public class GetProductHandler : IGetProductHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _memoryCache;

    public GetProductHandler(IProductRepository productRepository, IMemoryCache memoryCache)
    {
        _productRepository = productRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        await _memoryCache.GetOrCreateAsync($"product_{request.Id}", async entry =>
        {
            ProductDto productDto = await _productRepository.GetProductAsync(request.Id);
            Product product = CommonMapper.MapToProduct(productDto);
            entry.Value = product;
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return product;
        });
}