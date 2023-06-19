using MediatR;

namespace CatalogService.Core.Commands.Category;

public record DeleteCategoryCommand(int Id) : IRequest<bool>;