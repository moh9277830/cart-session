namespace Server.Application.Cart;

public class AddToCartRequest
{
    public int StockId { get; set; }
    public int Qty { get; set; }
}

public interface IAddToCartRequestHandler
{
    Task<bool> Handle(AddToCartRequest request);
}

public class AddToCartRequestHandler : IAddToCartRequestHandler
{
    private readonly ISessionManager _sessionManager;
    private readonly IStockManager _stockManager;

    public AddToCartRequestHandler(ISessionManager sessionManager, IStockManager stockManager)
    {
        _sessionManager = sessionManager;
        _stockManager = stockManager;
    }

    public async Task<bool> Handle(AddToCartRequest request)
    {
        if (!_stockManager.EnoughStock(request.StockId, request.Qty))
        {
            return false;
        }

        await _stockManager.PutStockOnHold(request.StockId, request.Qty, _sessionManager.GetId());

        var stock = _stockManager.GetStockWithProduct(request.StockId);

        if (stock == null)
        {
            return false;
        }

        var cartProduct = new CartProduct
        {
            ProductId = stock.ProductId,
            Name = stock.Product.Name,
            StockId = request.StockId,
            Qty = request.Qty,
            Value = stock.Product.UnitPrice
        };

        _sessionManager.AddProduct(cartProduct);

        return true;
    }
}