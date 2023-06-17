using CatalogService.Core.Commands.Product;
using CatalogService.Infrastructure.MsSql.Products;
using MediatR;

namespace CatalogService.Core.Handlers.Products;

public interface IDeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
}

public class DeleteProductHandler : IDeleteProductHandler
{
    private readonly IProductRepository _productRepository;
    public DeleteProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        await _productRepository.Delete(request.Id);
}