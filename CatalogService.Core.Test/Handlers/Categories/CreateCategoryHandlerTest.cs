using CatalogService.Core.Commands;
using CatalogService.Core.Handlers.Categories;
using CatalogService.Infrastructure.MsSql.Categories;
using Moq;

namespace CatalogService.Core.Test.Handlers.Categories;

public class CreateCategoryHandlerTest
{
    private const int DummyId = 42;
    [Fact]
    public async Task HandleAsync_ReturnsExpected()
    {
        // Arrange
        ICreateCategoryHandler categoryHandler = new CreateCategoryHandler(GetRepositoryMock().Object);

        // Act
        int result = await categoryHandler.HandleAsync(new CategoryCommand());

        // Assert
        Assert.Equal(DummyId, result);
    }

    private static Mock<ICategoryRepository> GetRepositoryMock()
    {
        var repository = new Mock<ICategoryRepository>();
        repository
            .Setup(x => x.CreateNew(It.IsAny<CreateUpdateCategoryCommand>()))
            .ReturnsAsync(DummyId);

        return repository;
    }
}