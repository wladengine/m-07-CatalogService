dotnet ef dbcontext scaffold "Server=localhost;Database=CatalogService;Integrated Security=True;TrustServerCertificate=True;Application Name=dotnet-ef-dbcontext-scaffold" Microsoft.EntityFrameworkCore.SqlServer `
 --table dbo.Category `
 --table dbo.Product `
 --table dbo.ProductCategory `
 --output-dir "MsSql\DatabaseModels" --context CatalogServiceContext --context-dir "MsSql" --context-namespace "CatalogService.Infrastructure.MsSql" --project "CatalogService.Infrastructure\CatalogService.Infrastructure.csproj" --startup-project "CatalogService.Infrastructure\CatalogService.Infrastructure.csproj" --force