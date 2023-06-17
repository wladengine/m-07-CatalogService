using CatalogService.Core.Commands.Product;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.MsSql.Products;
using MediatR;

namespace CatalogService.Core.Handlers.Products;

public interface IUpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
{
}

public class UpdateProductHandler : IUpdateProductHandler
{
    private readonly IProductRepository _productRepository;
    public UpdateProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        return CommonMapper.MapToProduct(
            await _productRepository.Update(CommonMapper.MapToDbCommand(request)));
    }
}