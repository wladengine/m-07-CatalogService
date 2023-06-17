using MediatR;

namespace CatalogService.Core.Queries.Product;

public record GetProductByIdQuery(int Id) : IRequest<Entities.Product>;