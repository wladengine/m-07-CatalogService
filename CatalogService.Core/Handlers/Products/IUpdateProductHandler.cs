using CatalogService.Core.Commands.Product;
using CatalogService.Core.Entities;
using MediatR;

namespace CatalogService.Core.Handlers.Products;

public interface IUpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
{
}