namespace Server.Application.StockAdmin;

public class DeleteStockRequest
{
    public int Id { get; set; }
}

public interface IDeleteStockRequestHandler
{
    Task<bool> Handle(DeleteStockRequest request);
}

public class DeleteStockRequestHandler: IDeleteStockRequestHandler
{
    private readonly IAppDbContext _db;

    public DeleteStockRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteStockRequest request)
    {
        var stock = _db.Stocks.FirstOrDefault(x => x.Id == request.Id);

        if (stock == null)
        {
            return false;
        }

        _db.Stocks.Remove(stock);

        await _db.SaveChangesAsync();

        return true;
    }
}
