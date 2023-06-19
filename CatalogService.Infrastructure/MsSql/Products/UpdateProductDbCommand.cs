namespace CatalogService.Infrastructure.MsSql.Products;

public record UpdateProductDbCommand(
    int Id,
    string Name,
    string Description,
    decimal Price,
    IEnumerable<int> Categories);