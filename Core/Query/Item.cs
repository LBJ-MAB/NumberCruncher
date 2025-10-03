namespace Core.Query;

public class Item
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
}

// making an immutable record called ItemV2
public record ItemV2 (
    int ProductId,
    string ProductName,
    int Quantity,
    float Price);