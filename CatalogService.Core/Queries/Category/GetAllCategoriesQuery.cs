using MediatR;

namespace CatalogService.Core.Queries.Category;

public record GetAllCategoriesQuery() : IRequest<Entities.Category[]>;