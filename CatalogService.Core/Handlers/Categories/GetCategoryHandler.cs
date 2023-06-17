using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface IGetCategoryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
{
}

public class GetCategoryHandler : IGetCategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository) =>
        _categoryRepository = categoryRepository;

    public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        CategoryDto category = await _categoryRepository.GetCategoryAsync(request.Id);
        return CommonMapper.MapToCategory(category);
    }
}