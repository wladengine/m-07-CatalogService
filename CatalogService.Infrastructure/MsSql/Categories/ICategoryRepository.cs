using CatalogService.Infrastructure.Dto;

namespace CatalogService.Infrastructure.MsSql.Categories;

public interface ICategoryRepository
{
    Task<CategoryDto[]> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryAsync(int categoryId);
    public Task<CategoryDto[]> GetCategoriesByProductAsync(int productId);
    Task<CategoryDto> CreateNewAsync(CreateCategoryDbCommand dbCommand);
    Task<CategoryDto> UpdateAsync(UpdateCategoryDbCommand dbCommand);
    Task<bool> DeleteAsync(int categoryId);
}