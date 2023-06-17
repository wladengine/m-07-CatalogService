using MediatR;

namespace CatalogService.Core.Queries.Category;

public record GetCategoriesByProductIdQuery(int ProductId) : IRequest<Entities.Category[]>;