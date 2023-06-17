using MediatR;

namespace CatalogService.Core.Commands.Product;

public record CreateProductCommand(
    string Name, 
    string Description, 
    decimal Price, 
    List<int> CategoryIds) : IRequest<Entities.Product>;