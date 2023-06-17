using CatalogService.Core.Commands.Product;
using MediatR;

namespace CatalogService.Core.Handlers.Products;

public interface IDeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
}