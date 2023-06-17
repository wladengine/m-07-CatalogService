using MediatR;

namespace CatalogService.Core.Commands.Product;

public record UpdateProductCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    List<int> CategoryIds) : IRequest<Entities.Product>;