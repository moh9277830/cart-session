using Server.Extensions;

namespace Server.Application.Cart;

public class GetCartRequest { }

public interface IGetCartRequestHandler
{
    IEnumerable<GetCartRequestHandler.Response> Handle(GetCartRequest request);
}

public class GetCartRequestHandler : IGetCartRequestHandler
{
    private readonly ISessionManager _sessionManager;

    public GetCartRequestHandler(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public IEnumerable<Response> Handle(GetCartRequest request)
    {
        return _sessionManager.GetCart(x => new Response
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Value = x.Value.GetValueString(),
            RealValue = x.Value,
            Qty = x.Qty,
            StockId = x.StockId,
        });
    }

    public class Response
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StockId { get; set; }
        public int Qty { get; set; }
        public string Value { get; set; } = string.Empty;
        public decimal RealValue { get; set; }
    }
}
