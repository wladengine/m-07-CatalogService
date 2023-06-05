using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public interface IGetProductsByCategoryHandler
{
    Task<IEnumerable<Product>> HandleAsync(int categoryId);
}

public class GetProductsByCategoryHandler : IGetProductsByCategoryHandler
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryHandler(IProductRepository productRepository) => 
        _productRepository = productRepository;

    public async Task<IEnumerable<Product>> HandleAsync(int categoryId)
    {
        IEnumerable<ProductDto> categories = await _productRepository.GetProductsByCategory(categoryId);
        return categories.Select(CommonMapper.MapToProduct).ToArray();
    }
}