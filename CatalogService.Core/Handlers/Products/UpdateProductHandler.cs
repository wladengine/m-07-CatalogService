using CatalogService.Core.Commands.Product;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public class UpdateProductHandler : IUpdateProductHandler
{
    private readonly IProductRepository _productRepository;
    public UpdateProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        return CommonMapper.MapToProduct(
            await _productRepository.UpdateAsync(CommonMapper.MapToDbCommand(request)));
    }
}