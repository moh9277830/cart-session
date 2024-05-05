namespace Server.Models;

public class GetCartResponse
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; } = 0;
    public int StockId { get; set; } = 0;
    public int Qty { get; set; } = 0;
}