using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public interface IGetProductHandler
{
    Task<Product> HandleAsync(int productId);
}

public class GetProductHandler : IGetProductHandler
{
    private readonly IProductRepository _productRepository;

    public GetProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<Product> HandleAsync(int productId)
    {
        ProductDto product = await _productRepository.GetProductAsync(productId);
        return CommonMapper.MapToProduct(product);
    }
}