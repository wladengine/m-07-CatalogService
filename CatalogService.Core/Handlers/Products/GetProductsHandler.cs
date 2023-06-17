using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Product;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public class GetProductsHandler : IGetProductsHandler
{
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductDto> products = await _productRepository.GetProductsAsync();
        return products.Select(CommonMapper.MapToProduct);
    }
}