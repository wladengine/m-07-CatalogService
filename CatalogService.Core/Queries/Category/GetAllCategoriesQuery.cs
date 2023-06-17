using MediatR;

namespace CatalogService.Core.Queries.Category;

public record GetAllCategoriesQuery() : IRequest<IEnumerable<Entities.Category>>;