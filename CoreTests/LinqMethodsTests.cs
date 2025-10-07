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
    public void FindFirstOrderExceedingTest()
    {
        // define minimum amount
        float minAmount = 570.0f;
        // define actual result
        OrderDetails firstOrderExceeding = _testQuery.FindFirstOrderExceeding(_actualList, minAmount);
        
        // should not be null
        firstOrderExceeding.Should().NotBeNull();
        // name should be John Doe
        firstOrderExceeding.CustomerName.Should().Be("John Doe");
        // id should be 1
        firstOrderExceeding.OrderId.Should().Be(1);
        // date should be 2023-10-01
        firstOrderExceeding.OrderDate.Should().Be("2023-10-01");
        // total amount should be approx 1251.00
        firstOrderExceeding.TotalAmount.Should().BeApproximately(1251.00f, 0.01f);
        // Items.ElementAt should satisfy ...
        firstOrderExceeding.Items.ElementAt(0).Should().Satisfy<Item>(item =>
        {
            item.ProductId.Should().Be(101);
            item.ProductName.Should().Be("Laptop");
            item.Quantity.Should().Be(1);
            item.Price.Should().BeApproximately(1200.00f, 0.01f);
        });
    }

    [Test]
    public void GetNameAndDateAfterThreeTest()
    {
         // define actual result
         List<NameAndDateDTO> nameAndDateDTOs = _testQuery.GetNameAndDateAfterThree(_actualList);
         
         // John Doe name and date DTO
         NameAndDateDTO JohnDoe = new NameAndDateDTO(CustomerName: "John Doe", OrderDate: "2023-10-01");
         // Jane Smith name and date DTO
         NameAndDateDTO JaneSmith = new NameAndDateDTO(CustomerName: "Jane Smith", OrderDate: "2023-10-02");
         // Alice Johnson name and date DTO
         NameAndDateDTO AliceJohnson = new NameAndDateDTO(CustomerName: "Alice Johnson", OrderDate: "2023-10-01");
         // Bob Brown name and date DTO
         NameAndDateDTO BobBrown = new NameAndDateDTO(CustomerName: "Bob Brown", OrderDate: "2023-10-01");
         
         
         // should not contain
         nameAndDateDTOs.Should().NotContain(nameAndDate => nameAndDate == AliceJohnson);
         // should not be empty
         nameAndDateDTOs.Should().NotBeEmpty();
         // should HaveCount (7)
         nameAndDateDTOs.Should().HaveCount(7);
         // elementAt should satisfy
         nameAndDateDTOs.ElementAt(2).Should().Satisfy<NameAndDateDTO>(dto =>
         {
             dto.CustomerName.Should().Be("Diana White");
             dto.OrderDate.Should().Be("2023-10-01");
         });
         // should start with
         nameAndDateDTOs.Should().StartWith(BobBrown);
    }
}