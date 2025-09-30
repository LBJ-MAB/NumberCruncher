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
        string testString = "67, gh, sww, 98";
        Assert.That(result, Is.EqualTo(Statistics.ConvertStringToArray(testString)));

        // Assert.That(Statistics.ConvertStringToArray(testString), Is.Empty);

        // Assert.That(Statistics.ConvertStringToArray(testString), Has.Exactly(3).Items);

        // Assert.That(Statistics.ConvertStringToArray(testString), Has.None.EqualTo(584));
    }

    [Test]
    public void MeanTest()
    {
        Dictionary<double, double[]> testDict = new Dictionary<double, double[]>
        {
            { 2, [1, 2, 3] },
            { 10, [5, 10, 15] },
            { 175, [200, 150, 100, 250] },
            { 4.65, [1.2, 9.3, 5.4, 2.7] },
            { -196.975682, [-1, -1000, 8, 4.98, 3.14159] },
            { 4.087, [1.31, 2.47, 3.91, 4.44, 5.62, 6.77] }
        };

        foreach (var pair in testDict)
        {
            double expected = pair.Key;
            double[] input = pair.Value;
            Assert.That(expected, Is.EqualTo(Statistics.Mean(input)).Within(0.001d));
        }
    }

    // [Test]
    // public void MedianTest()
    // {
    //     Dictionary<double, double[]> testDict = new Dictionary<double, double[]>
    //     {
    //         { , [1, 2, 3] },
    //         { , [5, 10, 15] },
    //         { , [200, 150, 100, 250] },
    //         { , [1.2, 9.3, 5.4, 2.7] },
    //         { , [-1, -1000, 8, 4.98, 3.14159] },
    //         { , [1.31, 2.47, 3.91, 4.44, 5.62, 6.77] }
    //     };
    //
    //     foreach (var pair in testDict)
    //     {
    //         double expected = pair.Key;
    //         double[] input = pair.Value;
    //         Assert.That(expected, Is.EqualTo(Statistics.Median(input)).Within(0.001d));
    //     }
    // }

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