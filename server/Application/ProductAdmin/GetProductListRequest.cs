using Microsoft.EntityFrameworkCore;

namespace Server.Application.ProductAdmin;

public class GetProductListRequest
{
}

public interface IGetProductListRequestHandler
{
    Task<IEnumerable<GetProductListRequestHandler.ProductDto>> Handle(GetProductListRequest request);
}

public class GetProductListRequestHandler : IGetProductListRequestHandler
{
    private readonly IAppDbContext _db;

    public GetProductListRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductListRequest request)
    {
        IQueryable<ProductDto> products = _db.Products
            .TagWith("Get all products with stock information")
            .AsNoTracking()
            .Include(x => x.Stocks)
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

        return await products.ToListAsync();
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
