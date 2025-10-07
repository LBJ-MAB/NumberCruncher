using System.Text.Json;
using Core;

namespace Core.Query;

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

        return orders!;
    }
    
    // Where method to filter by orderId
    public List<OrderDetails> FilterByOrderId(List<OrderDetails> orderDetails, int orderId)
    {
        var filteredByOrderId = orderDetails.Where(order => order.OrderId == orderId);
        Console.WriteLine(filteredByOrderId.GetType());
        return filteredByOrderId.ToList();
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
    
    // use the sum method to sum total Amounts 
    public double SumTotalAmounts(List<OrderDetails> orders)
    {
        double sum = orders.Sum(order => order.TotalAmount);
        return sum;
    }
    
    // get total # of items bought
    public int TotalItemsBought(List<OrderDetails> orders)
    {
        int totalItemsBought = 0;
        
        orders.ForEach((order) =>
        {
            int numOrderItems = order.Items.Sum(item => item.Quantity);
            totalItemsBought += numOrderItems;
        });

        return totalItemsBought;
    }
    
    // convert to NameAndDateDTO method - delegate function
    private Func<OrderDetails, NameAndDateDTO> ConvertToNameAndDateDTO = order => new NameAndDateDTO(order.CustomerName, order.OrderDate);
    
    // try a projection to a DTO - NameAndDate - using .skip() method as well
    public List<NameAndDateDTO> GetNameAndDateAfterThree(List<OrderDetails> orders)
    {
        List<NameAndDateDTO> nameAndDateAfterThree = orders.Skip(3).Select(ConvertToNameAndDateDTO).ToList();
        return nameAndDateAfterThree;
    }
    
    // convert to NameAndTotalAmountDTO method - delegate function
    private Func<OrderDetails, NameAndTotalAmountDTO> ConvertToNameAndTotalAmountDTO = order => new NameAndTotalAmountDTO(order.CustomerName, order.TotalAmount);
    
    // try another projection to DTO - NameAndTotalAmount - using .take() this time
    public List<NameAndTotalAmountDTO> GetFirstThreeNameAndTotalAmount(List<OrderDetails> orders)
    {
        List<NameAndTotalAmountDTO> firstThreeNameAndTotalAmount = orders.Take(3).Select(ConvertToNameAndTotalAmountDTO).ToList();
        return firstThreeNameAndTotalAmount;
    }
    
    // using .Concat() to concatenate two lists
    public List<OrderDetails> ConcatOrderLists(List<List<OrderDetails>> listOfOrderLists)
    {
        // empty list to start off with
        List<OrderDetails> concatenatedList = new List<OrderDetails>();
        
        // concatenate lists
        foreach (List<OrderDetails> orderList in listOfOrderLists)
        {
            concatenatedList = concatenatedList.Concat(orderList).ToList();
        }

        return concatenatedList;
    }
    
    // using .Union() to merge two lists without duplicates
    public List<OrderDetails> UnionTwoOrderLists(List<OrderDetails> orders1, List<OrderDetails> orders2)
    {
        return orders1.Union(orders2).ToList();
    }
    
    // intersect method - common elements from both collections
    public List<OrderDetails> IntersectTwoOrderLists(List<OrderDetails> orders1, List<OrderDetails> orders2)
    {
        return orders1.Intersect(orders2).ToList();
    }
    
    // Except method - returns values present in first collection but not present in second collection
    public List<OrderDetails> InFirstButNotSecond(List<OrderDetails> orders1, List<OrderDetails> orders2)
    {
        return orders1.Except(orders2).ToList();
    }
    
    
    // USE IEnumerable<> RATHER THAN List<> AS WELL
    
    // method for generic filter using pattern matching
    public IEnumerable<OrderDetails> Filter(List<OrderDetails> orders, Func<OrderDetails, bool> filterCriteriaFunction)
    {
        return orders.Where(filterCriteriaFunction);
    }

    // method for group (ToLookup) -> return a general ILookup depending on type of variable given? <T>

    // sorting (order by, order by descending) -> pattern matching for ascending / descending
    public IEnumerable<OrderDetails> Sort<T>(List<OrderDetails> orders, string sortDirection, Func<OrderDetails, T> sortProperty) =>
        sortDirection switch
        {
            "ascending" => orders.OrderBy(sortProperty),
            "descending" => orders.OrderByDescending(sortProperty),
            _ => throw new ArgumentException("Invalid string value for sortDirection")
        };

}