namespace CatalogService.Infrastructure.MsSql.Products;

public record CreateProductDbCommand(
    string Name,
    string Description,
    decimal Price,
    List<int> Categories);