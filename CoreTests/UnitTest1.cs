using Core;
using Core.Query;
using FluentAssertions;

namespace CoreTests;

public class Tests
{
    private List<OrderDetails> _actualList;
    
    [SetUp]
    public void Setup()
    {
        // define new query object
        Query testQuery = new Query();
        
        // define json file path
        string jsonFilePath = "orders.json";
        
        // define actual list
        _actualList = testQuery.ReadAndParseJsonIntoList(jsonFilePath);
    }
    
    [Test]
    public void ConvertStringToArrayTest()
    {
        double[]? result = [ 583, 743, 721 ];
        string testString = "rh, dl, pt";
        double[]? numArray = Statistics.ConvertStringToArray(testString);

        // numArray.Should().HaveCount(3);      // has 3 values
        // numArray.Should().NotBeEmpty();      // is not empty
        // numArray.Should().Equal(result);     // is equal to
        numArray.Should().BeNullOrEmpty(); // is null
    }

    [Test]
    public void MeanTest()
    {
        double result = -2459.3;
        double[] testArray = { -57, -6738, -583 };
        double mean = Statistics.Mean(testArray);

        mean.Should().BeApproximately(result, 0.1f);
    }

    [Test]
    public void MedianTest()
    {
        double result = 12;
        double[] testArray = { 16, 20, 4, 8, 12  };
        double median = Statistics.Median(testArray);

        median.Should().BeApproximately(result, 0.001f);
    }

    [Test]
    public void ModeTest()
    {
        double[]? result = [ 2, 3 ];
        double[] testArray = { 1, 2, 2, 3, 3 };
        double[]? modeArray = Statistics.Mode(testArray);

        modeArray.Should().Equal(result);               // Equal
        modeArray.Should().NotBeEmpty().And.HaveCount(2);        // not empty, and have count 2
        modeArray.Should().OnlyHaveUniqueItems();       // only unique items
        modeArray.Should().StartWith(2);                // start with
        modeArray.Should().EndWith(3);                  // end with
        modeArray.Should().BeSubsetOf(testArray);       // be subset of 
        modeArray.Should().NotContain(1);               // not contain
    }

    [Test]
    public void MinTest()
    {
        double result = 19;
        double[] testArray = { 90, 57, 31, 19, 78 };
        double min = Statistics.Min(testArray);

        min.Should().BeApproximately(result, 0.001f);
    }

    [Test]
    public void MaxTest()
    {
        double result = 90;
        double[] testArray = { 90, 57, 31, 19, 78 };
        double max = Statistics.Max(testArray);

        max.Should().BeApproximately(result, 0.001f);
    }

    [Test]
    public void StdevTest()
    {
        double result = 2.236;
        double[] testArray = { 2, 4, 6, 8 };
        double stdev = Statistics.Stdev(testArray);

        stdev.Should().BeApproximately(result, 0.001f);
        stdev.Should().BePositive();
    }
    
    // --- testing the query methods ---
    [Test]
    public void TestOrderDetailsAreCorrect()
    {
        // expected output for orderId, customerName, orderDate and totalAmount
        int expectedOrderId = 1;
        string expectedCustomerName = "John Doe";
        string expectedOrderDate = "2023-10-01";
        float expectedTotalAmount = 1251.00f;
        
        // assertions for matching expected values
        _actualList[0].OrderId.Should().Be(expectedOrderId, $"because this object in json file has order id of {expectedOrderId}");
        _actualList[0].CustomerName.Should().Be(expectedCustomerName, $"because this object in json file has customer name of {expectedCustomerName}");
        _actualList[0].OrderDate.Should().Be(expectedOrderDate, $"because this object in json file has order date of {expectedOrderDate}");
        _actualList[0].TotalAmount.Should().Be(expectedTotalAmount, $"because this object in json file has total amount of {expectedTotalAmount}");
    }

    [Test]
    public void TestOrderDetailsListNotEmpty()
    {
        _actualList.Should().NotBeEmpty().And.HaveCount(10, "Because there are 10 items in the json file");
    }

    [Test]
    public void TestOrderDetailsUnhappyPath()
    {
        // just so long as number is not 10
        _actualList.Should().NotHaveCount(7);
    }
}