using CatalogService.Core.Commands.Product;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.Products;

namespace CatalogService.Core.Handlers.Products;

public class CreateProductHandler : ICreateProductHandler
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository) =>
        _productRepository = productRepository;

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductDto productDto = await _productRepository.CreateNewAsync(CommonMapper.MapToDbCommand(request));
        return CommonMapper.MapToProduct(productDto);
    }
}