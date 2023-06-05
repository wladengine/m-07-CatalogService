using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public interface IDeleteCategoryHandler
{
    Task<bool> HandleAsync(int id);
}

public class DeleteCategoryHandler : IDeleteCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<bool> HandleAsync(int id) =>
        await _categoryRepository.Delete(id);
}