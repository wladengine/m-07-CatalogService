using CatalogService.Infrastructure.Dto;
using CatalogService.Infrastructure.MsSql.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.MsSql.Products;

public interface IProductRepository
{
    public Task<IEnumerable<ProductDto>> GetProductsAsync();
    public Task<ProductDto> GetProductAsync(int productId);
    Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId);
    public Task<ProductDto> CreateNew(CreateProductDbCommand dbCommand);
    public Task<ProductDto> Update(UpdateProductDbCommand dbCommand);
    public Task<bool> Delete(int productId);
}

public class ProductRepository : IProductRepository
{
    private readonly CatalogServiceContext _context;

    public ProductRepository(CatalogServiceContext context) => 
        _context = context;

    public async Task<IEnumerable<ProductDto>> GetProductsAsync() =>
        await _context.Products
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

    public async Task<ProductDto> GetProductAsync(int productId) =>
        await _context.Products
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

    public async Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId) =>
        await _context.Products
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

    public async Task<ProductDto> CreateNew(CreateProductDbCommand dbCommand)
    {
        List<Category> categories = await _context.Categories
            .Where(x => dbCommand.Categories.Contains(x.Id))
            .ToListAsync();

        var prod = new Product
        {
            Name = dbCommand.Name,
            Description = dbCommand.Description,
            Price = dbCommand.Price,
            Categories = categories
        };

        _context.Products.Add(prod);
        await _context.SaveChangesAsync();

        return MapToProductDto(prod);
    }

    public async Task<ProductDto> Update(UpdateProductDbCommand dbCommand)
    {
        Product? product = await _context.Products
            .Include(x => x.Categories)
            .SingleOrDefaultAsync(x => x.Id == dbCommand.Id);

        if (product == null)
        {
            return null;
        }

        List<Category> categories = await _context.Categories
            .Where(x => dbCommand.Categories.Contains(x.Id))
            .ToListAsync();

        product.Name = dbCommand.Name;
        product.Description = dbCommand.Description;
        product.Price = dbCommand.Price;

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

        await _context.SaveChangesAsync();

        return MapToProductDto(product);
    }

    private static ProductDto MapToProductDto(Product product)
    {
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
        Product? product = await _context.Products
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
        await _context.SaveChangesAsync();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
}