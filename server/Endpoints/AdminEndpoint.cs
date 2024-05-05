using Microsoft.AspNetCore.Mvc;

namespace Server.Endpoints;

public class AdminEndpoint
{
    // public static IResult GetProduct(HttpContext context)
    // {
    //     var id = context.Session.GetString("id");

    //     return Results.Ok($"cart product retrieved from session with id: {id}");
    // }

    // public static IResult AddProduct(HttpContext context, ISessionManager sessionManager, [FromBody] AddToCartRequest request)
    // {
    //     sessionManager.AddToCart(request.StockId, request.Qty);

    //     return Results.Ok();
    // }

    // public static IResult RemoveProduct()
    // {
    //     return Results.Ok("cart product removed");
    // }

    public static async Task<IResult> GetProduct([FromServices] IGetProductRequestHandler query, [FromRoute] int id) => 
        Results.Ok(await query.Handle(new GetProductRequest{ Id = id }));
    
    public static async Task<IResult> GetProductList([FromServices] IGetProductListRequestHandler query) => 
        Results.Ok(await query.Handle(new GetProductListRequest()));

    public static async Task<IResult> CreateProduct([FromServices] ICreateProductRequestHandler command, [FromBody] CreateProductRequest request) => 
        Results.Ok(await command.Handle(request));
    
    public static async Task<IResult> UpdateProduct([FromServices] IUpdateProductRequestHandler command, [FromBody] UpdateProductRequest request) => 
        Results.Ok(await command.Handle(request));
    
    public static async Task<IResult> DeleteProduct([FromServices] IDeleteProductRequestHandler command, [FromRoute] int id) => 
        Results.Ok(await command.Handle(new DeleteProductRequest { Id = id }));



    public static async Task<IResult> GetStock([FromServices] IGetStockRequestHandler query) => 
        Results.Ok(await query.Handle(new GetStockRequest()));

    public static async Task<IResult> CreateStock([FromServices] ICreateStockRequestHandler command, [FromBody] CreateStockRequest request) => 
        Results.Ok(await command.Handle(request));
    
    public static async Task<IResult> UpdateStock([FromServices] IUpdateStockRequestHandler command, [FromBody] UpdateStockRequest request) => 
        Results.Ok(await command.Handle(request));
    
    public static async Task<IResult> DeleteStock([FromServices] IDeleteStockRequestHandler command, [FromRoute] int id) => 
        Results.Ok(await command.Handle(new DeleteStockRequest { Id = id }));
}