using CatalogService.Core.Commands;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public interface IUpdateCategoryHandler
{
    Task<Category> HandleAsync(int id, CategoryCommand categoryCommand);
}

public class UpdateCategoryHandler : IUpdateCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<Category> HandleAsync(int id, CategoryCommand categoryCommand)
    {
        return CommonMapper.MapToCategory(
            await _categoryRepository.Update(id, CommonMapper.MapToCommand(categoryCommand)));
    }
}