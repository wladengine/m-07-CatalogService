using MediatR;

namespace CatalogService.Core.Commands.Product;

public record UpdateProductCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    IEnumerable<int> CategoryIds) : IRequest<Entities.Product>;