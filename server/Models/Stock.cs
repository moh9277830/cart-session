namespace Server.Models;

public class Stock
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Qty { get; set; }

    public Product Product { get; set; } = default!;
}