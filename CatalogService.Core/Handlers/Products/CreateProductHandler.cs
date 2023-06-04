using CatalogService.Core.Commands;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public interface ICreateProductHandler
{
    Task<int> HandleAsync(ProductCommand product);
}

public class CreateProductHandler : ICreateProductHandler
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<int> HandleAsync(ProductCommand product)
    {
        return await _productRepository.CreateNew(CommonMapper.MapToCommand(product));
    }
}