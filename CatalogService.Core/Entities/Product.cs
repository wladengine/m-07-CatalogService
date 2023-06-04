namespace CatalogService.Core.Entities;

public record Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } = 0;
    public IEnumerable<CategoryBrief> Categories { get; set; }
}