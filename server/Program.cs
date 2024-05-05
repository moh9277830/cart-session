using Microsoft.EntityFrameworkCore;
using Server.Application.Cart;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddScoped<ISessionManager, SessionManager>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<AppDbSeeder>();
builder.Services.AddScoped<ICreateProductRequestHandler, CreateProductRequestHandler>();
builder.Services.AddScoped<IGetProductListRequestHandler, GetProductListRequestHandler>();
builder.Services.AddScoped<IGetProductRequestHandler, GetProductRequestHandler>();
builder.Services.AddScoped<IUpdateProductRequestHandler, UpdateProductRequestHandler>();
builder.Services.AddScoped<IDeleteProductRequestHandler, DeleteProductRequestHandler>();

builder.Services.AddTransient<IStockManager, StockManager>();

builder.Services.AddScoped<ICreateStockRequestHandler, CreateStockRequestHandler>();
builder.Services.AddScoped<IGetStockRequestHandler, GetStockRequestHandler>();
builder.Services.AddScoped<IUpdateStockRequestHandler, UpdateStockRequestHandler>();
builder.Services.AddScoped<IDeleteStockRequestHandler, DeleteStockRequestHandler>();

builder.Services.AddScoped<IGetCartRequestHandler, GetCartRequestHandler>(); 
builder.Services.AddScoped<IAddToCartRequestHandler, AddToCartRequestHandler>(); 
builder.Services.AddScoped<IUpdateCartRequestHandler, UpdateCartRequestHandler>(); 
builder.Services.AddScoped<IRemoveFromCartRequestHandler, RemoveFromCartRequestHandler>(); 

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o =>
{
    // o.IdleTimeout = TimeSpan.FromMinutes(30);
    o.Cookie.MaxAge = TimeSpan.FromMinutes(20);
    o.Cookie.Name = "CartSession";
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseSession();

var apiGroup = app.MapGroup("/api");

apiGroup.MapPost("/products", AdminEndpoint.CreateProduct);
apiGroup.MapGet("/products", AdminEndpoint.GetProductList);
apiGroup.MapGet("/products/{id}", AdminEndpoint.GetProduct);
apiGroup.MapPut("/products", AdminEndpoint.UpdateProduct);
apiGroup.MapDelete("/products/{id}", AdminEndpoint.DeleteProduct);

apiGroup.MapPost("/stocks", AdminEndpoint.CreateStock);
apiGroup.MapGet("/stocks", AdminEndpoint.GetStock);
apiGroup.MapPut("/stocks", AdminEndpoint.UpdateStock);
apiGroup.MapDelete("/stocks/{id}", AdminEndpoint.DeleteStock);

apiGroup.MapGet("/session", SessionEnpoint.GetSession);
apiGroup.MapPost("/cart/{productId}", CartEndpoint.AddProduct);
apiGroup.MapPut("/cart/{productId}", CartEndpoint.UpdateProduct);
apiGroup.MapGet("/cart", CartEndpoint.GetCartProducts);
apiGroup.MapDelete("/cart/{stockId}/{qty}", CartEndpoint.RemoveProduct);

apiGroup.MapGet("/status/{status}", (string status) => Results.StatusCode(int.Parse(status)));

if (app.Environment.IsDevelopment())
{
    var uiDevServer = app.Configuration.GetValue<string>("UiDevServerUrl");
    if (!string.IsNullOrEmpty(uiDevServer))
    {
        app.MapReverseProxy();
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    var dbSeeder = services.GetRequiredService<AppDbSeeder>();
    if (dbContext.Database.GetMigrations().Any())
    {
        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            await dbSeeder.SeedAsync();
        }
    }
}

app.Run();