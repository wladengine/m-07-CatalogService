using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public interface IDeleteProductHandler
{
    Task<bool> HandleAsync(int id);
}

public class DeleteProductHandler : IDeleteProductHandler
{
    private readonly IProductRepository _productRepository;
    public DeleteProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<bool> HandleAsync(int id) =>
        await _productRepository.Delete(id);
}