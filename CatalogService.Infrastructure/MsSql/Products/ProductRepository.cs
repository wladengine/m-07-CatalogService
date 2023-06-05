using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.MsSql.Products;

public interface IProductRepository
{
    public Task<IEnumerable<ProductDto>> GetProductsAsync();
    public Task<ProductDto> GetProductAsync(int productId);
    Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId);
    public Task<int> CreateNew(CreateUpdateProductCommand command);
    public Task<ProductDto> Update(int productId, CreateUpdateProductCommand command);
    public Task<bool> Delete(int productId);
}

public class ProductRepository : IProductRepository
{
    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        await using var context = new CatalogServiceContext();
        return await context.Products
            .Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Categories = x.Categories.Select(b => new CategoryBriefDto
                {
                    Id = b.Id,
                    Name = b.Name,
                })
            })
            .ToArrayAsync();
    }

    public async Task<ProductDto> GetProductAsync(int productId)
    {
        await using var context = new CatalogServiceContext();
        return await context.Products
            .Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Categories = x.Categories.Select(b => new CategoryBriefDto
                {
                    Id = b.Id,
                    Name = b.Name,
                })
            })
            .SingleAsync(p => p.Id == productId);
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId)
    {
        await using var context = new CatalogServiceContext();
        return await context.Products
            .Where(x => x.Categories.Any(c => c.Id == categoryId))
            .Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Categories = x.Categories.Select(b => new CategoryBriefDto
                {
                    Id = b.Id,
                    Name = b.Name,
                })
            })
            .ToArrayAsync();
    }

    public async Task<int> CreateNew(CreateUpdateProductCommand command)
    {
        await using var context = new CatalogServiceContext();
        List<Category> categories = await context.Categories
            .Where(x => command.Categories.Contains(x.Id))
            .ToListAsync();

        var prod = new Product
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Categories = categories
        };

        context.Products.Add(prod);
        await context.SaveChangesAsync();
        
        return prod.Id;
    }

    public async Task<ProductDto> Update(int productId, CreateUpdateProductCommand command)
    {
        await using var context = new CatalogServiceContext();

        Product? product = await context.Products
            .Include(x => x.Categories)
            .SingleOrDefaultAsync(x => x.Id == productId);

        if (product == null)
        {
            return null;
        }

        List<Category> categories = await context.Categories
            .Where(x => command.Categories.Contains(x.Id))
            .ToListAsync();

        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;

        foreach (Category category in categories)
        {
            if (product.Categories.All(c => c.Id != category.Id))
            {
                product.Categories.Add(category);
            }
        }

        List<Category> toDelete = new(
            product.Categories.Where(category => categories.All(c => c.Id != category.Id)));

        foreach (Category category in toDelete)
        {
            product.Categories.Remove(category);
        }

        await context.SaveChangesAsync();

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Categories = product.Categories.Select(b => new CategoryBriefDto
            {
                Id = b.Id,
                Name = b.Name,
            })
        };
    }

    public async Task<bool> Delete(int productId)
    {
        await using var context = new CatalogServiceContext();

        Product? product = await context.Products
            .Include(c => c.Categories)
            .SingleOrDefaultAsync(x => x.Id == productId);

        if (product == null)
        {
            return false;
        }
        
        List<Category> categories = product.Categories.ToList();
        foreach (Category category in categories)
        {
            product.Categories.Remove(category);
        }
        await context.SaveChangesAsync();
        
        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return true;
    }
}