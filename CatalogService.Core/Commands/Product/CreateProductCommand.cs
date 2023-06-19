using MediatR;

namespace CatalogService.Core.Commands.Product;

public record CreateProductCommand(
    string Name, 
    string Description, 
    decimal Price, 
    IEnumerable<int> CategoryIds) : IRequest<Entities.Product>;