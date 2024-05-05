namespace Server.Application.ProductAdmin;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
}

public interface ICreateProductRequestHandler
{
    Task<CreateProductRequestHandler.Response> Handle(CreateProductRequest request);
}

public class CreateProductRequestHandler: ICreateProductRequestHandler
{
    private readonly IAppDbContext _db;

    public CreateProductRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<Response> Handle(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            UnitPrice = request.UnitPrice
        };
        
        _db.Products.Add(product);

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
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
