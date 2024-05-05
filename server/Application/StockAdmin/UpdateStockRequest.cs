namespace Server.Application.StockAdmin;

public class UpdateStockRequest
{
    public IEnumerable<UpdateStockRequestHandler.StockDto> Stock { get; set; } = [];
}

public interface IUpdateStockRequestHandler
{
    Task<UpdateStockRequestHandler.Response> Handle(UpdateStockRequest request);
}

public class UpdateStockRequestHandler: IUpdateStockRequestHandler
{
    private readonly IAppDbContext _db;

    public UpdateStockRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<Response> Handle(UpdateStockRequest request)
    {
        var stocks = new List<Stock>();

        foreach (var stock in request.Stock)
        {
            stocks.Add(new Stock
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                Description = stock.Description,
                Qty = stock.Qty
            });
        }

        _db.Stocks.UpdateRange(stocks);

        await _db.SaveChangesAsync();

        return new Response
        {
            Stock = request.Stock
        };
    }

    public class StockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Qty { get; set; }
    }

    public class Response
    {
        public IEnumerable<StockDto> Stock { get; set; } = [];
    }
}
