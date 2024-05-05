namespace Server.Application.StockAdmin;

public class CreateStockRequest
{
    public int ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Qty { get; set; }
}

public interface ICreateStockRequestHandler
{
    Task<CreateStockRequestHandler.Response> Handle(CreateStockRequest request);
}

public class CreateStockRequestHandler: ICreateStockRequestHandler
{
    private readonly IAppDbContext _db;

    public CreateStockRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<Response> Handle(CreateStockRequest request)
    {
        var stock = new Stock
        {
            ProductId = request.ProductId,
            Description = request.Description,
            Qty = request.Qty
        };

        _db.Stocks.Add(stock);

        await _db.SaveChangesAsync();

        return new Response
        {
            Id = stock.Id,
            ProductId = stock.ProductId,
            Description = stock.Description,
            Qty = stock.Qty
        };
    }

    public class Response
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Qty { get; set; }
    }
}
