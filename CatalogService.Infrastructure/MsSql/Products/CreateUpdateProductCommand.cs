namespace CatalogService.Infrastructure.MsSql.Products;

public class CreateUpdateProductCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } 
    public List<int> Categories { get; set; }
}