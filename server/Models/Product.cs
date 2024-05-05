namespace Server.Models;

public class Product
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }

    public ICollection<Stock> Stocks { get; set; } = new HashSet<Stock>();
}