using MediatR;

namespace CatalogService.Core.Commands.Category;

public record UpdateCategoryCommand(int Id, string Name, string Description) : IRequest<Entities.Category>;