namespace CatalogService.Infrastructure.Dto;

public class ProductDto : ProductBriefDto
{
    public IEnumerable<CategoryBriefDto> Categories { get; set; }
}