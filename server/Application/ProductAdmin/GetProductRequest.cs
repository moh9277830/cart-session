using Microsoft.EntityFrameworkCore;

namespace Server.Application.ProductAdmin;

public class GetProductRequest
{
    public int Id { get; set; }
}

public interface IGetProductRequestHandler
{
    Task<GetProductRequestHandler.ProductDto> Handle(GetProductRequest request);
}

public class GetProductRequestHandler : IGetProductRequestHandler
{
    private readonly IAppDbContext _db;

    public GetProductRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<ProductDto> Handle(GetProductRequest request)
    {
        IQueryable<ProductDto> products = _db.Products
            .TagWith("Get all products with stock information")
            .AsNoTracking()
            .Include(x => x.Stocks)
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                Stocks = x.Stocks.Select(y => new StockDto
                {
                    Id = y.Id,
                    Description = y.Description,
                    Qty = y.Qty
                })
            });

        var result = await products.FirstOrDefaultAsync();

         _ = result ?? throw new ArgumentException($"Service {request.Id} not found.");

        return result;
    }

    public class StockDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Qty { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; } = 0;
        public IEnumerable<StockDto> Stocks { get; set; } = [];
    }
}
