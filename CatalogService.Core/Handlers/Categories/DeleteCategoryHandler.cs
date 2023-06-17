using CatalogService.Core.Commands.Category;
using CatalogService.Infrastructure.MsSql.Categories;

namespace CatalogService.Core.Handlers.Categories;

public class DeleteCategoryHandler : IDeleteCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) => 
        await _categoryRepository.DeleteAsync(request.Id);
}