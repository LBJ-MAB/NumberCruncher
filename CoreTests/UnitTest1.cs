using Core;

namespace CoreTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void ConvertStringToArrayTest()
    {
        double[]? result = null;
        string testString = "1, 2, 3, gfhgh";
        Assert.That(result, Is.EqualTo(Statistics.ConvertStringToArray(testString)));

        // Assert.That(Statistics.ConvertStringToArray(testString), Is.Empty);

        // Assert.That(Statistics.ConvertStringToArray(testString), Has.Exactly(3).Items);

        // Assert.That(Statistics.ConvertStringToArray(testString), Has.None.EqualTo(584));
    }

    [Test]
    public void MeanTest()
    {
        double result = 2;
        double[] testArray = { 1, 2, 3 };
        Assert.That(result, Is.EqualTo(Statistics.Mean(testArray)));
    }

    [Test]
    public void MedianTest()
    {
        double result = 2;
        double[] testArray = { 1, 2, 3 };
        Assert.That(result, Is.EqualTo(Statistics.Median(testArray)));
    }

    [Test]
    public void ModeTest()
    {
        double result = 4;
        double[] testArray = { 1, 6, 4, 4, 3, 4, 8, 4, 9, 4 };
        Assert.That(result, Is.EqualTo(Statistics.Mode(testArray)));
    }

    [Test]
    public void MinTest()
    {
        double result = 19;
        double[] testArray = { 90, 57, 31, 19, 78 };
        Assert.That(result, Is.EqualTo(Statistics.Min(testArray)));
    }

    [Test]
    public void MaxTest()
    {
        double result = 90;
        double[] testArray = { 90, 57, 31, 19, 78 };
        Assert.That(result, Is.EqualTo(Statistics.Max(testArray)));
    }

    [Test]
    public void StdevTest()
    {
        double result = 2.236;
        double[] testArray = { 2, 4, 6, 8 };
        Assert.That(result, Is.EqualTo(Statistics.Stdev(testArray)).Within(0.001d));
    }
}