using MediatR;

namespace CatalogService.Core.Queries.Product;

public record GetAllProductsQuery() : IRequest<IEnumerable<Entities.Product>>;