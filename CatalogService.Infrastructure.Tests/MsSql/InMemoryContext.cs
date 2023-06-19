using CatalogService.Infrastructure.MsSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure.Tests.MsSql;

public class InMemoryContext : CatalogServiceContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        optionsBuilder.UseInMemoryDatabase(databaseName: "TestDataBase")
            .UseInternalServiceProvider(serviceProvider);
    }
}