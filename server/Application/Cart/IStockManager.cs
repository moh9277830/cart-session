namespace Server.Application.Cart;

public interface IStockManager
{
    Stock? GetStockWithProduct(int stockId);
    bool EnoughStock(int stockId, int qty);
    Task PutStockOnHold(int stockId, int qty, string sessionId);
    Task<bool> RemoveStockFromHold(int stockId, int qty, string sessionId);
}