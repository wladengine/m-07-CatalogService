using CatalogService.Core.Commands.Category;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.MsSql.Categories;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface IUpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
{
}

public class UpdateCategoryHandler : IUpdateCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken) =>
        CommonMapper.MapToCategory(
            await _categoryRepository.Update(CommonMapper.MapToDbCommand(request)));
}