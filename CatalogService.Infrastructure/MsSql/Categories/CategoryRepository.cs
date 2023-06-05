using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.MsSql.Categories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto> GetCategoryAsync(int categoryId);
    public Task<IEnumerable<CategoryDto>> GetCategoriesByProduct(int productId);
    Task<int> CreateNew(CreateUpdateCategoryCommand command);
    Task<CategoryDto> Update(int categoryId, CreateUpdateCategoryCommand command);
    Task<bool> Delete(int categoryId);
}

public class CategoryRepository : ICategoryRepository
{
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        await using var context = new CatalogServiceContext();
        return await context.Categories
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
    }

    public async Task<CategoryDto> GetCategoryAsync(int categoryId)
    {
        await using var context = new CatalogServiceContext();
        return await context.Categories
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
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByProduct(int productId)
    {
        await using var context = new CatalogServiceContext();
        return await context.Categories
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
    }

    public async Task<int> CreateNew(CreateUpdateCategoryCommand command)
    {
        await using var context = new CatalogServiceContext();

        var category = new Category
        {
            Name = command.Name,
            Description = command.Description,
            Products = new List<Product>()
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return category.Id;
    }

    public async Task<CategoryDto> Update(int categoryId, CreateUpdateCategoryCommand command)
    {
        await using var context = new CatalogServiceContext();

        Category? category = await context.Categories
            .Include(x => x.Products)
            .SingleOrDefaultAsync(x => x.Id == categoryId);

        if (category == null)
        {
            return null;
        }

        category.Name = command.Name;
        category.Description = command.Description;

        await context.SaveChangesAsync();

        return new CategoryDto
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
    }

    public async Task<bool> Delete(int categoryId)
    {
        await using var context = new CatalogServiceContext();

        Category? category = await context.Categories
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

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return true;
    }
}