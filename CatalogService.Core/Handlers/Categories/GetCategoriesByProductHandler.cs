using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Category;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Categories;
using MediatR;

namespace CatalogService.Core.Handlers.Categories;

public interface IGetCategoriesByProductHandler : IRequestHandler<GetCategoriesByProductIdQuery, IEnumerable<Category>>
{
}

public class GetCategoriesByProductHandler : IGetCategoriesByProductHandler
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesByProductHandler(ICategoryRepository categoryRepository) => 
        _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Category>> Handle(GetCategoriesByProductIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CategoryDto> categoriesDto = await _categoryRepository.GetCategoriesByProduct(request.ProductId);
        return categoriesDto.Select(CommonMapper.MapToCategory).ToArray();
    }
}