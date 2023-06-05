namespace CatalogService.Infrastructure.Dto;

public class CategoryDto : CategoryBriefDto
{
    public string Description { get; set; }
    public IEnumerable<ProductBriefDto> Products { get; set; }
}