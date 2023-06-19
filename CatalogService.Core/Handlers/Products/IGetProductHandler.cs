using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Product;
using MediatR;

namespace CatalogService.Core.Handlers.Products;

public interface IGetProductHandler : IRequestHandler<GetProductByIdQuery, Product>
{
}