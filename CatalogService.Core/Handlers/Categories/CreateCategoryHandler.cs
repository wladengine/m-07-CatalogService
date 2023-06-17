using CatalogService.Core.Commands.Category;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.MsSql.Categories;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface ICreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
{
}

public class CreateCategoryHandler : ICreateCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) =>
        CommonMapper.MapToCategory(
            await _categoryRepository.CreateNew(CommonMapper.MapToDbCommand(request)));
}