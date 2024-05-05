namespace Server.Application.Cart;

public class RemoveFromCartRequest
{
    public int StockId { get; set; }
    public int Qty { get; set; }
}

public interface IRemoveFromCartRequestHandler
{
    Task<bool> Handle(RemoveFromCartRequest request);
}

public class RemoveFromCartRequestHandler : IRemoveFromCartRequestHandler
{
    private readonly ISessionManager _sessionManager;
    private readonly IStockManager _stockManager;

    public RemoveFromCartRequestHandler(ISessionManager sessionManager, IStockManager stockManager)
    {
        _sessionManager = sessionManager;
        _stockManager = stockManager;
    }

    public async Task<bool> Handle(RemoveFromCartRequest request)
    {
        if(request.Qty <= 0)
        {
            return false;
        }

        await _stockManager.RemoveStockFromHold(request.StockId, request.Qty, _sessionManager.GetId());

        _sessionManager.RemoveProduct(request.StockId, request.Qty);

        return true;
    }
}