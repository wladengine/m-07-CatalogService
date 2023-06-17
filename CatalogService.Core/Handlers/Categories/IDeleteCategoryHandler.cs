using CatalogService.Core.Commands.Category;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface IDeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
}