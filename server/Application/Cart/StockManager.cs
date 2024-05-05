using Microsoft.EntityFrameworkCore;

namespace Server.Application.Cart;

public class StockManager : IStockManager
{
    private readonly IAppDbContext _db;

    public StockManager(IAppDbContext db)
    {
        _db = db;
    }

    public Stock? GetStockWithProduct(int stockId)
    {
        var query = _db.Stocks
            .Include(x => x.Product)
            .Where(x => x.Id == stockId);

        return query.FirstOrDefault();
    }

    public bool EnoughStock(int stockId, int qty)
    {
        var stock = _db.Stocks.FirstOrDefault(x => x.Id == stockId);

        if (stock == null) { return false; }

        return stock.Qty >= qty;
    }

    public Task PutStockOnHold(int stockId, int qty, string sessionId)
    {
        // upate Stock set qty = qty - {0} where id = {1}
        var actualStock = _db.Stocks.FirstOrDefault(x => x.Id == stockId);
        if (actualStock != null)
        {
            actualStock.Qty -= qty;
        }

        var stockOnHold = _db.StocksOnHold
            .Where(x => x.SessionId == sessionId)
            .ToList();

        // select count(*) from StockOnHold where StockId = {0} and SessionId = {1}
        if (stockOnHold.Any(x => x.StockId == stockId))
        {
            // upate StocksOnHold set qty = qty + {0}
            // where StockId = {1} and SessionId = {2}
            var stock = stockOnHold.Find(x => x.StockId == stockId);
            if (stock != null)
            {
                stock.Qty += qty;
            }
        }
        else
        {
            // insert into StocksOnHold (StockId, SessionId, Qty, ExpiryDate)
            // values ({0}, {1}, {2}, {3})
            _db.StocksOnHold.Add(new StockOnHold
            {
                StockId = stockId,
                SessionId = sessionId,
                Qty = qty,
                ExpiryDate = DateTimeOffset.UtcNow.AddMinutes(20)
            });
        }

        // update StocksOnHold set ExpiryDate = {0} where SessionId = {1}
        foreach (var stock in stockOnHold)
        {
            stock.ExpiryDate = DateTimeOffset.UtcNow.AddMinutes(20);
        }

        // commit transaction
        return _db.SaveChangesAsync();
    }

    public async Task<bool> RemoveStockFromHold(int stockId, int qty, string sessionId)
    {
        var stockOnHold = _db.StocksOnHold
            .FirstOrDefault(x => x.StockId == stockId && x.SessionId == sessionId);

        if (stockOnHold == null) { return false; }
        
        var stock = _db.Stocks.Find(stockId);
        
        if(stock == null) { return false; }
        
        stock.Qty += qty;
        stockOnHold.Qty -= qty;

        if(stockOnHold.Qty <= 0)
        {
            _db.StocksOnHold.Remove(stockOnHold);
        }

        await _db.SaveChangesAsync();

        return true;
    }
}