using CatalogService.Core.Commands;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public interface IUpdateProductHandler
{
    Task<Product> HandleAsync(int id, ProductCommand product);
}

public class UpdateProductHandler : IUpdateProductHandler
{
    private readonly IProductRepository _productRepository;
    public UpdateProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<Product> HandleAsync(int id, ProductCommand product)
    {
        return CommonMapper.MapToProduct(
            await _productRepository.Update(id, CommonMapper.MapToCommand(product)));
    }
}