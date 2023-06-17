using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.MsSql.Categories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryAsync(int categoryId);
    public Task<IEnumerable<CategoryDto>> GetCategoriesByProduct(int productId);
    Task<CategoryDto> CreateNew(CreateCategoryDbCommand dbCommand);
    Task<CategoryDto> Update(UpdateCategoryDbCommand dbCommand);
    Task<bool> Delete(int categoryId);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogServiceContext _context;

    public CategoryRepository(CatalogServiceContext context) => 
        _context = context;

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync() =>
        await _context.Categories
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Products = x.Products.Select(b => new ProductBriefDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                })
            })
            .ToArrayAsync();

    public async Task<CategoryDto> GetCategoryAsync(int categoryId) =>
        await _context.Categories
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Products = x.Products.Select(b => new ProductBriefDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                })
            })
            .SingleAsync(p => p.Id == categoryId);

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByProduct(int productId) =>
        await _context.Categories
            .Where(x => x.Products.Any(c => c.Id == productId))
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Products = x.Products.Select(b => new ProductBriefDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    Description = b.Description,
                })
            })
            .ToArrayAsync();

    public async Task<CategoryDto> CreateNew(CreateCategoryDbCommand dbCommand)
    {
        var category = new Category
        {
            Name = dbCommand.Name,
            Description = dbCommand.Description,
            Products = new List<Product>()
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return ConvertToCategoryDto(category);
    }

    public async Task<CategoryDto> Update(UpdateCategoryDbCommand dbCommand)
    {
        Category? category = await _context.Categories
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.Id == dbCommand.Id);

        if (category == null)
        {
            return null;
        }

        category.Name = dbCommand.Name;
        category.Description = dbCommand.Description;

        await _context.SaveChangesAsync();

        return ConvertToCategoryDto(category);
    }

    private static CategoryDto ConvertToCategoryDto(Category category) =>
        new()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Products = category.Products.Select(b => new ProductBriefDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Price = b.Price,
            })
        };

    public async Task<bool> Delete(int categoryId)
    {
        Category? category = await _context.Categories
            .Include(c => c.Products)
            .SingleOrDefaultAsync(x => x.Id == categoryId);

        if (category == null)
        {
            return false;
        }

        bool hasProducts = category.Products.Any();
        if (hasProducts)
        {
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }
}