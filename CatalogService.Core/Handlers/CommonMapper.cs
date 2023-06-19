using CatalogService.Core.Commands.Category;
using CatalogService.Core.Commands.Product;
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

    public static CreateProductDbCommand MapToDbCommand(CreateProductCommand command) => new(
        command.Name,
        command.Description,
        command.Price,
        command.CategoryIds);

    public static UpdateProductDbCommand MapToDbCommand(UpdateProductCommand command) => new(
        command.Id,
        command.Name,
        command.Description,
        command.Price,
        command.CategoryIds);

    public static CreateCategoryDbCommand MapToDbCommand(CreateCategoryCommand command) =>
        new(command.Name, command.Description);

    public static UpdateCategoryDbCommand MapToDbCommand(UpdateCategoryCommand command) => 
        new(command.Id, command.Name, command.Description);
}