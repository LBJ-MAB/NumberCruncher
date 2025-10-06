using System.Text.Json;
using Core;

namespace Core.Query;

/*
 * TRY USING RECORDS INSTEAD OF CLASSES FOR ORDERDETAILS AND ITEM
 */

// class for dealing with reading in and parsing json
public class Query
{
    // json serializer object properties
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    // method for reading in json and returning a list of OrderDetails objects
    public List<OrderDetails> ReadAndParseJsonIntoList(string jsonFilePath)
    {
        // define json 
        string json = File.ReadAllText(jsonFilePath);
        
        // deserialize json into List of OrderDetails() objects
        List<OrderDetails>? orders = JsonSerializer.Deserialize<List<OrderDetails>>(json, _options);

        return orders;
    }
    
    // method for printing the details of an order
    public void PrintOrderDetails(OrderDetails order)
    {
        Console.WriteLine("Order Id : {0}", order.OrderId);
        Console.WriteLine("Customer Name : {0}", order.CustomerName);
        Console.WriteLine("Order date : {0}", order.OrderDate);
        Console.WriteLine("Items: ");
        foreach (Item item in order.Items)
        {
            Console.WriteLine("  {0}x {1} for {2:C}", item.Quantity, item.ProductName, item.Price);
        }
        Console.WriteLine("Total Amount : {0}\n", order.TotalAmount);
    }
    
    // Where method to filter by orderId
    public List<OrderDetails> FilterByOrderId(List<OrderDetails> orderDetails, int orderId)
    {
        var filteredByOrderId = orderDetails.Where(order => order.OrderId == orderId).ToList();
        return filteredByOrderId;
    }
    
    // Where method to filter by minimum spend
    public List<OrderDetails> FilterByMinimumSpend(List<OrderDetails> orderDetails, float minimumTotal)
    {
        var filteredByMinimumSpend = orderDetails.Where(order => order.TotalAmount > minimumTotal).ToList();
        return filteredByMinimumSpend;
    }
    
    // OrderByDescending method for sorting by total amount
    public List<OrderDetails> SortByTotalAmount(List<OrderDetails> orderDetails)
    {
        var sortedByTotalAmount = orderDetails.OrderByDescending(order => order.TotalAmount).ToList();
        return sortedByTotalAmount;
    }
    
    // OrderByDescending method for number of distinct items bought
    public List<OrderDetails> SortByNumberOfDistinctItems(List<OrderDetails> orderDetails)
    {
        var sortedByNumberOfDistinctItems = orderDetails.OrderByDescending(order => order.Items.Count).ToList();
        return sortedByNumberOfDistinctItems;
    }
    
    // First method for finding first order that exceeds a minimum total amount
    public OrderDetails FindFirstOrderExceeding(List<OrderDetails> orderDetails, float minimumAmount)
    {
        var firstOrderWhere = orderDetails.First(order => order.TotalAmount > minimumAmount);
        return firstOrderWhere;
    }
    
    // Using ForEach to map to a new list on whether each person spent more than the mean spent
    public List<bool> SpentMoreThanMean(List<OrderDetails> orderDetails)
    {
        // make a new list of just total Amounts
        List<double> totalAmounts = new List<double>();
        orderDetails.ForEach(order => totalAmounts.Add(order.TotalAmount));

        // turn it into array
        double[] totalAmountsArr = totalAmounts.ToArray();

        // calculate mean on the array
        double meanTotalAmount = Statistics.Mean(totalAmountsArr);

        // define bool list
        List<bool> spentMoreThanMeanList = new List<bool>();
        orderDetails.ForEach(order => spentMoreThanMeanList.Add(order.TotalAmount > meanTotalAmount));

        return spentMoreThanMeanList;
    }
    
    // use the sum method 
    public double SumTotalAmounts(List<OrderDetails> orders)
    {
        double sum = orders.Sum(order => order.TotalAmount);
        return sum;
    }
    
    
    
    
    
    
    
    // method for filter
    
    
    // method for map
    
    // method for group
    
    // method for join
    
    // projections to DTOs
    
    // sorting
    
    // pagination
}