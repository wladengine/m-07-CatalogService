namespace CatalogService.Infrastructure.MsSql.Categories;

public record UpdateCategoryDbCommand(int Id, string Name, string Description);