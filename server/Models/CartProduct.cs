namespace Server.Models;

public class CartProduct
{
    public int ProductId { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public int StockId { get; set; } = 0;
    public int Qty { get; set; } = 0;
    public decimal Value { get; set; } = 0;
}