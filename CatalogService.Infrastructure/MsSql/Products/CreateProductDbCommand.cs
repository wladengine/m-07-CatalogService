namespace CatalogService.Infrastructure.MsSql.Products;

public record CreateProductDbCommand(
    string Name,
    string Description,
    decimal Price,
    IEnumerable<int> Categories);