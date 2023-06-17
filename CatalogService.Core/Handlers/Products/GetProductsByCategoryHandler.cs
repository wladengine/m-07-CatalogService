using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Product;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;
using MediatR;

namespace CatalogService.Core.Handlers.Products;

public interface IGetProductsByCategoryHandler : IRequestHandler<GetProductsByCategoryIdQuery, IEnumerable<Product>>
{
}

public class GetProductsByCategoryHandler : IGetProductsByCategoryHandler
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryHandler(IProductRepository productRepository) => 
        _productRepository = productRepository;

    public async Task<IEnumerable<Product>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductDto> categories = await _productRepository.GetProductsByCategory(request.CategoryId);
        return categories.Select(CommonMapper.MapToProduct).ToArray();
    }
}