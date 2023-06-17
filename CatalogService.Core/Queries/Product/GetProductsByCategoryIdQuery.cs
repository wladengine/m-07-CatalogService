using MediatR;

namespace CatalogService.Core.Queries.Product;

public record GetProductsByCategoryIdQuery(int CategoryId) : IRequest<IEnumerable<Entities.Product>>;