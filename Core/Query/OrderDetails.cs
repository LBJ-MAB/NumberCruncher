namespace Core.Query;

public class OrderDetails
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string OrderDate { get; set; }
    public List<Item> Items { get; set; }
    public float TotalAmount { get; set; }
}