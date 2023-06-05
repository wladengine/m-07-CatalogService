using CatalogService.Core.Commands;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;
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

    public static CreateUpdateProductCommand MapToCommand(ProductCommand productCommand) => new()
    {
        Name = productCommand.Name,
        Description = productCommand.Description,
        Price = productCommand.Price,
        Categories = productCommand.CategoryIds
    };

    public static Category MapToCategory(CategoryDto category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        Products = category.Products.Select(MapToProductBrief).ToArray(),
    };

    public static ProductBrief MapToProductBrief(ProductBriefDto product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
    };

    public static CreateUpdateCategoryCommand MapToCommand(CategoryCommand categoryCommand) => new()
    {
        Name = categoryCommand.Name,
        Description = categoryCommand.Description,
    };
}