using CatalogService.Infrastructure.Dto;

namespace CatalogService.Infrastructure.MsSql.Products;

public interface IProductRepository
{
    public Task<ProductDto[]> GetProductsAsync();
    public Task<ProductDto> GetProductAsync(int productId);
    Task<ProductDto[]> GetProductsByCategory(int categoryId);
    public Task<ProductDto> CreateNewAsync(CreateProductDbCommand dbCommand);
    public Task<ProductDto> UpdateAsync(UpdateProductDbCommand dbCommand);
    public Task<bool> DeleteAsync(int productId);
}