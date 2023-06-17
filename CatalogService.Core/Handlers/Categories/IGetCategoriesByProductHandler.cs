using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface IGetCategoriesByProductHandler : IRequestHandler<GetCategoriesByProductIdQuery, Category[]>
{
}