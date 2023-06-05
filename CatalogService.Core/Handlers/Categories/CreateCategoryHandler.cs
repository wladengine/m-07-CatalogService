using CatalogService.Core.Commands;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public interface ICreateCategoryHandler
{
    Task<int> HandleAsync(CategoryCommand product);
}

public class CreateCategoryHandler : ICreateCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<int> HandleAsync(CategoryCommand product)
    {
        return await _categoryRepository.CreateNew(CommonMapper.MapToCommand(product));
    }
}