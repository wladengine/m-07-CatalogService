using MediatR;

namespace CatalogService.Core.Commands.Category;

public record CreateCategoryCommand(string Name, string Description) : IRequest<Entities.Category>;