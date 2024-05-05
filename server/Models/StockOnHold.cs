namespace Server.Models;

public class StockOnHold
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public string SessionId { get; set; } = default!;
    public int Qty { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }

    public Stock Stock { get; set; } = default!;
}