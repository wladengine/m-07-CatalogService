using CatalogService.Infrastructure.Dto;

namespace CatalogService.Infrastructure.MsSql.Products;

public interface IProductRepository
{
    public Task<IEnumerable<ProductDto>> GetProductsAsync();
    public Task<ProductDto> GetProductAsync(int productId);
    Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId);
    public Task<ProductDto> CreateNewAsync(CreateProductDbCommand dbCommand);
    public Task<ProductDto> UpdateAsync(UpdateProductDbCommand dbCommand);
    public Task<bool> DeleteAsync(int productId);
}