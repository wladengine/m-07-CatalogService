using CatalogService.Core.Commands.Category;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public class CreateCategoryHandler : ICreateCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) =>
        CommonMapper.MapToCategory(
            await _categoryRepository.CreateNewAsync(CommonMapper.MapToDbCommand(request)));
}