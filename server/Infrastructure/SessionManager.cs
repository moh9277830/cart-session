using System.Text.Json;

namespace Server.Infrastructure;

public class SessionManager : ISessionManager
{
    private readonly ISession _session;

    public SessionManager(IHttpContextAccessor httpContextAccessor)
    {
        _session = httpContextAccessor.HttpContext?.Session ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    
    public string GetId() => _session.Id;

    public void AddProduct(CartProduct cartProduct)
    {
        var cartList = new List<CartProduct>();
        var stringObject = _session.GetString("cart");

        if (!string.IsNullOrEmpty(stringObject))
        {
            cartList = JsonSerializer.Deserialize<List<CartProduct>>(stringObject) ?? new List<CartProduct>();
        }

        if (cartList.Any(x => x.StockId == cartProduct.StockId))
        {
            var foundCartList = cartList.Find(x => x.StockId == cartProduct.StockId);
            if (foundCartList != null) foundCartList.Qty += cartProduct.Qty;
        }
        else
        {
            cartList.Add(cartProduct);
        }

        stringObject = JsonSerializer.Serialize(cartList);

        _session.SetString("cart", stringObject);
    }

    public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
    {
        var stringObject = _session.GetString("cart");

        if (string.IsNullOrEmpty(stringObject)) 
            return [];

        var cartList = JsonSerializer.Deserialize<IEnumerable<CartProduct>>(stringObject) ?? [];

        return cartList.Select(selector);
    }

    public void UpdateProduct(CartProduct cartProduct)
    {
        var cartList = new List<CartProduct>();
        var stringObject = _session.GetString("cart");

        if (!string.IsNullOrEmpty(stringObject))
        {
            cartList = JsonSerializer.Deserialize<List<CartProduct>>(stringObject) ?? new List<CartProduct>();
        }

        var foundCartList = cartList.Find(x => x.StockId == cartProduct.StockId);
        if (foundCartList != null)
        {
            foundCartList.Qty = cartProduct.Qty;
        }

        stringObject = JsonSerializer.Serialize(cartList);

        _session.SetString("cart", stringObject);
    }

    public void RemoveProduct(int stockId, int qty)
    {
        var cartList = new List<CartProduct>();
        var stringObject = _session.GetString("cart");

        if (!string.IsNullOrEmpty(stringObject))
        {
            cartList = JsonSerializer.Deserialize<List<CartProduct>>(stringObject) ?? new List<CartProduct>();
        }

        var foundCartList = cartList.Find(x => x.StockId == stockId);
        if (foundCartList != null)
        {
            foundCartList.Qty -= qty;
        }

        cartList = cartList.Where(x => x.Qty > 0).ToList();

        stringObject = JsonSerializer.Serialize(cartList);

        _session.SetString("cart", stringObject);
    }

    public void AddCustomerInformation(CustomerInformation customer)
    {
        var stringObject = JsonSerializer.Serialize(customer);

        _session.SetString("customer-info", stringObject);
    }

    public CustomerInformation? GetCustomerInformation()
    {
        var stringObject = _session.GetString("customer-info");

        if (string.IsNullOrEmpty(stringObject))
            return null;

        var customerInformation = JsonSerializer.Deserialize<CustomerInformation>(stringObject);

        return customerInformation;
    }
}