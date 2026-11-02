using App.Lab2;

namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.", new[] { "she left", "stood up", "then she", "she stood up", "then she left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a b", "b c", "c d", "e b", "a b c", "b c d", "e b c", "c a d" })]
    public void FindMostFrequentContinuations(string inputText, string[] dictionaryLines)
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

    [TestCase("")]
    [TestCase("Hello.")]
    public void TextWithoutWordPairs_ReturnsEmptyDictionary(string inputText)
    {
        var result = Frequency.FrequencyAnalysis(inputText);

        Assert.That(result, Is.Empty);
    }
}