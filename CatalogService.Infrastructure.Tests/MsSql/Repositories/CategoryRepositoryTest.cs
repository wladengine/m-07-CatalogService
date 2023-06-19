using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql;
using CatalogService.Infrastructure.MsSql.Categories;
using CatalogService.Infrastructure.MsSql.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CatalogService.Infrastructure.Tests.MsSql.Repositories;

public class CategoryRepositoryTest
{
    [Fact]
    public async Task GetCategoriesAsync_ReturnsCategories()
    {
        // Arrange
        await using var context = new InMemoryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        context.Categories.AddRange(GetDummyCategories());
        await context.SaveChangesAsync();

        // Act
        var repository = new CategoryRepository(context);
        CategoryDto[] result = (await repository.GetCategoriesAsync()).ToArray();

        // Assert
        Assert.Equal(3, result.Length);
        Assert.Contains(result, c => c.Name == "Category 1");
        Assert.Contains(result, c => c.Name == "Category 2");
        Assert.Contains(result, c => c.Name == "Category 3");
    }

    private static Category[] GetDummyCategories() =>
        new[]
        {
            new Category { Id = 1, Name = "Category 1", Description = "Category 1 description" },
            new Category { Id = 2, Name = "Category 2", Description = "Category 2 description" },
            new Category { Id = 3, Name = "Category 3", Description = "Category 3 description" }
        };
}