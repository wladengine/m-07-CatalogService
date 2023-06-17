using CatalogService.Core.Commands.Category;
using CatalogService.Infrastructure.MsSql.Categories;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface IDeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
}

public class DeleteCategoryHandler : IDeleteCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) => 
        await _categoryRepository.Delete(request.Id);
}