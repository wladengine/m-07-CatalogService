namespace CatalogService.Core.Commands;

public class ProductCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<int> CategoryIds { get; set; }
}