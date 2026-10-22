using App.Lab2;

namespace AppTests.Lab2;

public class LogAggregatorTests
{
    [Test]
    public void MergeLogs_OnlyGarbage_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs("garbage", "[invalid]", null);
        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MergeLogs_SingleGroup_SortsByTime()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }
    
    
    [Test]
    public void MergeLogs_MultipleGroups_SortsGroupsAndServices()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:00] [trace-B] Gateway: Start",
            "[2023-10-25 14:05:01] [trace-A] Billing: Payment",
            "[2023-10-25 14:05:01] [trace-A] AuthService: Login"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-B] ---",
            "[2023-10-25 14:05:00] Gateway: Start",
            "",
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: Login",
            "[2023-10-25 14:05:01] Billing: Payment",
            "[2023-10-25 14:05:02] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void MergeLogs_InvalidDate_IgnoresLine()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-99-25 14:05:01] [A] Auth: Invalid"
        );

        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public void MergeLogs_SameStartTime_SortsTraceIds()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:00] [trace-B] Gateway: Start",
            "[2023-10-25 14:05:00] [trace-A] Auth: Start"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:00] Auth: Start",
            "",
            "--- TraceId: [trace-B] ---",
            "[2023-10-25 14:05:00] Gateway: Start"
        });

        Assert.That(result, Is.EqualTo(expected));
    }
}