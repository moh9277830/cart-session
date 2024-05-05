namespace Server.Infrastructure.Persistence;

public class AppDbSeeder
{
    private readonly IAppDbContext _context;

    public AppDbSeeder(IAppDbContext context)
    {
        _context = context;
    }

    // public static async Task SeedProductsAsync(this IServiceScope scope)
    // {
    //     var db = scope.ServiceProvider.GetRequiredService<ProductContext>();
    // }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (_context.Products.Any())
        {
            return;
        }

        var products = new List<Product>
        {
            new Product
            {
                Name = "Product 1",
                UnitPrice = 100,
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Description = "small",
                        Qty = 10
                    }
                }
            },
            new Product
            {
                Name = "Product 2",
                UnitPrice = 200,
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Description = "medium",
                        Qty = 20
                    }
                }
            },
            new Product
            {
                Name = "Product 3",
                UnitPrice = 150,
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Description = "large",
                        Qty = 15
                    }
                }
            },
            new Product
            {
                Name = "Product 4",
                UnitPrice = 50,
                Stocks = new List<Stock>
                {
                    new Stock
                    {
                        Description = "x-large",
                        Qty = 5
                    }
                }
            }
        };

        await _context.Products.AddRangeAsync(products, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}