using App.Lab2;

namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.", new[] { "she left", "stood up", "then she", "she stood up", "then she left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a b", "b c", "c d", "e b", "a b c", "b c d", "e b c", "c a d" })]
    public void Tests(string inputText, string[] dictionaryLines)
    {
        var result = Frequency.FrequencyAnalysis(inputText);
        var resultLines = new List<string>();

        foreach (var entry in result)
        {
            resultLines.Add(entry.Key + " " + entry.Value);
        }

        Assert.That(resultLines, Is.EquivalentTo(dictionaryLines));
    }
    
    [Test]
    public void EmptyText_ReturnsEmptyDictionary()
    {
        var result = Frequency.FrequencyAnalysis("");

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void SingleWord_ReturnsEmptyDictionary()
    {
        var result = Frequency.FrequencyAnalysis("Hello.");

        Assert.That(result, Is.Empty);
    }
}