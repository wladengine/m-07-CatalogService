using MediatR;

namespace CatalogService.Core.Commands.Product;

public record DeleteProductCommand(int Id) : IRequest<bool>;