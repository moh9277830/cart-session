namespace Server.Application.ProductAdmin;

public class DeleteProductRequest
{
    public int Id { get; set; }
}

public interface IDeleteProductRequestHandler
{
    Task<bool> Handle(DeleteProductRequest request);
}

public class DeleteProductRequestHandler: IDeleteProductRequestHandler
{
    private readonly IAppDbContext _db;

    public DeleteProductRequestHandler(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteProductRequest request)
    {
        var product = _db.Products.FirstOrDefault(x => x.Id == request.Id);

        if (product == null)
        {
            return false;
        }

        _db.Products.Remove(product);

        await _db.SaveChangesAsync();

        return true;
    }
}
