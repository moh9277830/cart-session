namespace Server.Infrastructure;

public interface ISessionManager
{
    string GetId();
    void AddProduct(CartProduct cartProduct);
    void UpdateProduct(CartProduct cartProduct);
    void RemoveProduct(int stockId, int qty);
    IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);

    void AddCustomerInformation(CustomerInformation customer);
    CustomerInformation? GetCustomerInformation();
}