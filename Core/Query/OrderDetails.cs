namespace Core.Query;

public class OrderDetails
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string OrderDate { get; set; }
    public List<Item> Items { get; set; }
    public float TotalAmount { get; set; }
}

public record OrderDetailsV2
{
    int OrderId { get; set; }
    string CustomerName { get; set; }
    string OrderDate { get; set; }
    List<Item> Items { get; set; }
    float TotalAmount { get; set; }
}

// Immutable record
public record OrderDetailsV3(
    int OrderId,
    string CustomerName,
    List<Item> Items,
    float TotalAmount);
    
// You can make a copy using the "with" keyword
// var x = new OrderDetailsV3(10, "Jeff", [new Item(...)], 12.4);
// var y = x with CustomerName = "Bobby";

// Good for DTO: Data Transfer Object