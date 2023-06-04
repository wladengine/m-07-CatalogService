namespace CatalogService.Infrastructure.Dto;

public class CategoryDto : CategoryBriefDto
{
    public string Description { get; set; }
    public IEnumerable<ProductDto> Products { get; set; }
}