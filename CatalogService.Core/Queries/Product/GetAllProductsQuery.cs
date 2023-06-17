using MediatR;

namespace CatalogService.Core.Queries.Product;

public record GetAllProductsQuery() : IRequest<Entities.Product[]>;