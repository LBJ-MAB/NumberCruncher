using Core;
using Core.Query;
using FluentAssertions;

namespace CoreTests;

public class LinqMethodsTests
{
    private List<OrderDetails> _actualList;
    private Query _testQuery;
    
    [SetUp]
    public void Setup()
    {
        // define new query object
        _testQuery = new Query();
        
        // define json file path
        string jsonFilePath = "orders.json";
        
        // define actual list of orders
        _actualList = _testQuery.ReadAndParseJsonIntoList(jsonFilePath);
    }

    [Test]
    public void TestFilterMethod()
    {
        // filter function for date
        Func<OrderDetails, bool> dateIsOctoberFirst = order => order.OrderDate == "2023-10-01";
        
        // define actual result - filteredList
        List<OrderDetails> filteredList = _testQuery.Filter(_actualList, dateIsOctoberFirst).ToList();
        
        // should not be empty
        filteredList.Should().NotBeEmpty();
        // should have count
        filteredList.Should().HaveCount(6, "because there are 6 orders with this date");
        // should contain only the date "2023-10-01"
        filteredList.Should().OnlyContain(order => order.OrderDate == "2023-10-01");
        // should not contain date "2023-10-02"
        filteredList.Should().NotContain(order => order.OrderDate == "2023-10-02");
        // should only contain unique order ID's
        filteredList.Should().OnlyHaveUniqueItems(order => order.OrderId);
        // should start with
        filteredList.Select(order => new NameAndDateDTO(
            order.CustomerName,
            order.OrderDate))
            .Should().StartWith(new NameAndDateDTO(
                CustomerName : "John Doe",
                OrderDate : "2023-10-01"));
        // should end with
        filteredList.Select(order => new NameAndDateDTO(
                order.CustomerName,
                order.OrderDate))
            .Should().EndWith(new NameAndDateDTO(
                CustomerName : "Hannah Violet",
                OrderDate : "2023-10-01"));
    }

    [Test]
    public void TestSortMethod()
    {
        // function to sort by total amount
        Func<OrderDetails, float> totalAmount = order => order.TotalAmount;

        // define actual result
        List<OrderDetails> sortedList = _testQuery.Sort(_actualList, "descending", totalAmount).ToList();
        
        // should not be empty
        sortedList.Should().NotBeEmpty();
        // should have count
        sortedList.Should().HaveCount(10, "because there are 10 orders overall");
        // should only contain unique order ID's
        sortedList.Should().OnlyHaveUniqueItems(order => order.OrderId);
        // should start with
        sortedList.Select(order => new NameAndTotalAmountDTO(
                order.CustomerName, order.TotalAmount))
            .Should().StartWith(new NameAndTotalAmountDTO(
                CustomerName : "John Doe",
                TotalAmount : 1251.00f));
        // should end with
        sortedList.Select(order => new NameAndTotalAmountDTO(
                order.CustomerName, order.TotalAmount))
            .Should().EndWith(new NameAndTotalAmountDTO(
                CustomerName: "Jane Smith",
                TotalAmount: 45.00f));
        // should be / not be in ascending / descending order
        sortedList.Should().BeInDescendingOrder(order => order.TotalAmount);
        // should have element at
        sortedList.Select(order => new NameAndTotalAmountDTO(
                order.CustomerName, order.TotalAmount))
            .Should().HaveElementAt(4, new NameAndTotalAmountDTO(
                CustomerName : "Diana White",
                TotalAmount : 200.00f));
        // should have element preceeding
        sortedList.Select(order => new NameAndTotalAmountDTO(
                order.CustomerName, order.TotalAmount))
            .Should().HaveElementPreceding(
                new NameAndTotalAmountDTO(
                    CustomerName : "Hannah Violet",
                    TotalAmount : 125.00f),
                new NameAndTotalAmountDTO(
                    CustomerName : "Bob Brown",
                    TotalAmount : 150.00f));
        // should have element succeeding
        sortedList.Select(order => new NameAndTotalAmountDTO(
                order.CustomerName, order.TotalAmount))
            .Should().HaveElementSucceeding(
                new NameAndTotalAmountDTO(
                    CustomerName : "Hannah Violet",
                    TotalAmount : 125.00f),
                new NameAndTotalAmountDTO(
                    CustomerName : "Charlie Green",
                    TotalAmount : 70.00f));
    }
    
    [Test]
    
}