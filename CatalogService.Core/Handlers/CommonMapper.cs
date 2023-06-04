using CatalogService.Core.Commands;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers;

internal static class CommonMapper
{
    public static Product MapToProduct(ProductDto product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Categories = product.Categories.Select(MapToCategoryDto).ToArray(),
    };

    public static CategoryBrief MapToCategoryDto(CategoryBriefDto category) => new()
    {
        Id = category.Id,
        Name = category.Name,
    };

    public static CreateUpdateProductCommand MapToCommand(ProductCommand product) => new()
    {
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        Categories = product.CategoryIds
    };
}