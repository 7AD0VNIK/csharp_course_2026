using App;
using App.Lab1;

namespace AppTests.Lab1;

public class LeapYearTests
{
    [TestCase(2100, false)]
    [TestCase(2400, true)]
    [TestCase(2104, true)]
    [TestCase(1600, true)]
    [TestCase(1601, false)]
    [TestCase(2000, true)]
    [TestCase(1900, false)]
    [TestCase(2404, true)]
    [TestCase(2019, false)]
    [TestCase(2020, true)]
    [TestCase(2024, true)]
    [TestCase(2400, true)]
    [TestCase(2100, false)]
    
    public void TestPasses_When_Result_Correct(int year, bool expected)
    {
        var actual = LeapYear.IsLeapYear(year);
        Assert.That(actual, Is.EqualTo(expected));
    }
}