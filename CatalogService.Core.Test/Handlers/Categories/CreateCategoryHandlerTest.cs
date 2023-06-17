using CatalogService.Core.Commands.Category;
using CatalogService.Core.Entities;
using CatalogService.Core.Handlers.Categories;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;
using MediatR;
using Moq;

namespace CatalogService.Core.Tests.Handlers.Categories;

public class CreateCategoryHandlerTest
{
    private static readonly CategoryDto DummyCategory = new()
    {
        Id = 42,
        Name = nameof(Category.Name),
        Description = nameof(Category.Description),
        Products = new List<ProductBriefDto>()
    };

    [Fact]
    public async Task HandleAsync_ReturnsExpected()
    {
        // Arrange
        ICreateCategoryHandler categoryHandler = new CreateCategoryHandler(GetRepositoryMock().Object);

        // Act
        Category result = await categoryHandler.Handle(new CreateCategoryCommand("a", "b"), CancellationToken.None);

        // Assert
        Assert.Equal(DummyCategory.Id, result.Id);
        Assert.Equal(DummyCategory.Id, result.Id);
    }

    private static Mock<ICategoryRepository> GetRepositoryMock()
    {
        var repository = new Mock<ICategoryRepository>();
        repository
            .Setup(x => x.CreateNew(It.IsAny<CreateCategoryDbCommand>()))
            .ReturnsAsync(DummyCategory);

        return repository;
    }
}