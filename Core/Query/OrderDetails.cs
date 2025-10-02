namespace Core.Query;

public class OrderDetails
{
    public int orderId;
    public string customerName;
    public string orderDate;
    public List<Item> items;
    public float totalAmount;
}