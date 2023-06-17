using CatalogService.Core.Entities;
using CatalogService.Core.Queries.Product;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;
using MediatR;

namespace CatalogService.Core.Handlers.Products;

public interface IGetProductHandler : IRequestHandler<GetProductByIdQuery, Product>
{
}

public class GetProductHandler : IGetProductHandler
{
    private readonly IProductRepository _productRepository;

    public GetProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;


    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        ProductDto productDto = await _productRepository.GetProductAsync(request.Id);
        return CommonMapper.MapToProduct(productDto);
    }
}