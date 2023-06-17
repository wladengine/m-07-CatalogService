using CatalogService.Core;
using CatalogService.Core.Handlers.Categories;
using CatalogService.Core.Handlers.Products;
using CatalogService.Infrastructure.MsSql;
using CatalogService.Infrastructure.MsSql.Categories;
using CatalogService.Infrastructure.MsSql.Products;
using Microsoft.EntityFrameworkCore;

static void RegisterHandlers(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services
        .AddScoped<IGetProductsHandler, GetProductsHandler>()
        .AddScoped<IGetProductHandler, GetProductHandler>()
        .AddScoped<ICreateProductHandler, CreateProductHandler>()
        .AddScoped<IUpdateProductHandler, UpdateProductHandler>()
        .AddScoped<IDeleteProductHandler, DeleteProductHandler>()
        .AddScoped<IGetProductsByCategoryHandler, GetProductsByCategoryHandler>()

        .AddScoped<IGetCategoriesHandler, GetCategoriesHandler>()
        .AddScoped<IGetCategoryHandler, GetCategoryHandler>()
        .AddScoped<ICreateCategoryHandler, CreateCategoryHandler>()
        .AddScoped<IUpdateCategoryHandler, UpdateCategoryHandler>()
        .AddScoped<IDeleteCategoryHandler, DeleteCategoryHandler>()
        .AddScoped<IGetCategoriesByProductHandler, GetCategoriesByProductHandler>()

        .AddScoped<IProductRepository, ProductRepository>()
        .AddScoped<ICategoryRepository, CategoryRepository>();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogServiceContext>(
    builder => builder.UseSqlServer());

builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyEntryPoint).Assembly));

RegisterHandlers(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
