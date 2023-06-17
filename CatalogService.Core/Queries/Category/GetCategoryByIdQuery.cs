using MediatR;

namespace CatalogService.Core.Queries.Category;

public record GetCategoryByIdQuery(int Id) : IRequest<Entities.Category>;