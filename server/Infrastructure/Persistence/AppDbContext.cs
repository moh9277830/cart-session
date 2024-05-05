using Microsoft.EntityFrameworkCore;

namespace Server.Infrastructure.Persistence;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Stock> Stocks { get; }
    DbSet<StockOnHold> StocksOnHold { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<StockOnHold> StocksOnHold => Set<StockOnHold>();
}
