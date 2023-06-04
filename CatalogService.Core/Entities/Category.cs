namespace CatalogService.Core.Entities;

public record CategoryBrief
{
    public int Id { get; set; }
    public string Name { get; set; }
    
}

public record Category : CategoryBrief
{
    public string Description { get; set; }
    public IEnumerable<Product> Products { get; set; }
}