namespace Server.Application.ProductAdmin;

public class UpdateProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
}

public interface IUpdateProductRequestHandler
{
    Task<UpdateProductRequestHandler.Response> Handle(UpdateProductRequest request);
}

public class UpdateProductRequestHandler: IUpdateProductRequestHandler
{
    private readonly IAppDbContext _db;

    public UpdateProductRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<Response> Handle(UpdateProductRequest request)
    {
        var product = _db.Products.FirstOrDefault(x => x.Id == request.Id);

        _ = product ?? throw new ArgumentException($"Product {request.Id} not found.");
        
        product.Name = request.Name;
        product.UnitPrice = request.UnitPrice;

        _db.Products.Update(product);

        await _db.SaveChangesAsync();

        return new Response
        {
            Id = product.Id,
            Name = product.Name,
            UnitPrice = product.UnitPrice,
        };
    }


    public class Response
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
