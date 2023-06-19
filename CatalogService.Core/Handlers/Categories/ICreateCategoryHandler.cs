using CatalogService.Core.Commands.Category;
using CatalogService.Core.Entities;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface ICreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
{
}