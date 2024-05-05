using Microsoft.AspNetCore.Mvc;
using Server.Application.Cart;

namespace Server.Endpoints;

public class CartEndpoint
{
    public static async Task<IResult> AddProduct([FromServices] IAddToCartRequestHandler command, [FromBody] AddToCartRequest request) =>
        Results.Ok(await command.Handle(request));

    public static async Task<IResult> UpdateProduct([FromServices] IUpdateCartRequestHandler command, [FromBody] UpdateCartRequest request) =>
        Results.Ok(await command.Handle(request));

    public static IResult GetCartProducts([FromServices] IGetCartRequestHandler query) =>
        Results.Ok(query.Handle(new GetCartRequest()));

    public static async Task<IResult> RemoveProduct([FromServices] IRemoveFromCartRequestHandler command, int stockId, int qty)
    {
        var request = new RemoveFromCartRequest
        {
            StockId = stockId,
            Qty = qty
        };

        var result = await command.Handle(request);

        if (result)
        {
            return Results.Ok("Item removed from cart");
        }
        else
        {
            return Results.BadRequest("Not enough stock");
        }
    }
}