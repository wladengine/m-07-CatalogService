namespace CatalogService.Infrastructure.MsSql.Categories;

public class CreateUpdateCategoryCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
}