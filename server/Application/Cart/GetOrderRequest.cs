namespace Server.Application.Cart;

public class GetOrderRequest
{
}

public interface IGetOrderRequestHandler
{
    Task<GetOrderRequestHandler.Response> Handle(GetOrderRequest request);
}

public class GetOrderRequestHandler : IGetOrderRequestHandler
{
    private readonly ISessionManager _sessionManager;

    public GetOrderRequestHandler(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public Task<Response> Handle(GetOrderRequest request)
    {
        var listOfProducts = _sessionManager.GetCart(x => new Product
        {
            ProductId = x.ProductId,
            Name = x.Name,
            StockId = x.StockId,
            Qty = x.Qty,
            Value = (int)(x.Value * 100)
        }).ToList();

        var customerInformation = _sessionManager.GetCustomerInformation();

        var response = new Response
        {
            Products = listOfProducts,
            CustomerInformation = new CustomerInformation
            {
                FirstName = customerInformation?.FirstName ?? string.Empty,
                LastName = customerInformation?.LastName ?? string.Empty,
                Email = customerInformation?.Email ?? string.Empty,
                PhoneNumber = customerInformation?.PhoneNumber ?? string.Empty,
                Address1 = customerInformation?.Address1 ?? string.Empty,
                Address2 = customerInformation?.Address2 ?? string.Empty,
                City = customerInformation?.City ?? string.Empty,
                PostCode = customerInformation?.PostCode ?? string.Empty
            }
        };

        return Task.FromResult(response);
    }

    public class Response
    {
        public List<Product> Products { get; set; } = [];
        public CustomerInformation CustomerInformation { get; set; } = new();

        public int TotalCharge => Products.Sum(x => (int)x.Value * x.Qty);
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StockId { get; set; }
        public int Qty { get; set; }
        public decimal Value { get; set; }
    }
}