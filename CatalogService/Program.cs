using CatalogService.Core.Handlers.Products;
using CatalogService.Infrastructure.MsSql.Products;

static void RegisterHandlers(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services
        .AddScoped<IGetProductsHandler, GetProductsHandler>()
        .AddScoped<IGetProductHandler, GetProductHandler>()
        .AddScoped<ICreateProductHandler, CreateProductHandler>()
        .AddScoped<IUpdateProductHandler, UpdateProductHandler>()
        .AddScoped<IDeleteProductHandler, DeleteProductHandler>()
        .AddScoped<IProductRepository, ProductRepository>();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
