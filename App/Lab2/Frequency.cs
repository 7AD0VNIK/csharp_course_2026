namespace App.Lab2;

using System.Text;

public class Frequency
{
    public static Dictionary<string, string> FrequencyAnalysis(string inputString)
    {
        var result = new Dictionary<string, string>(StringComparer.Ordinal);

        if (string.IsNullOrEmpty(inputString))
        {
            return result;
        }

        var counts = new Dictionary<string, Dictionary<string, int>>(StringComparer.Ordinal);
        var words = new List<string>();
        var word = new StringBuilder();

        for (var i = 0; i <= inputString.Length; i++)
        {
            var isTextEnd = i == inputString.Length;
            var currentChar = isTextEnd ? '.' : inputString[i];

            if (char.IsLetterOrDigit(currentChar))
            {
                word.Append(char.ToLowerInvariant(currentChar));
                continue;
            }

            if (word.Length > 0)
            {
                words.Add(word.ToString());
                word.Clear();
            }

            if (isTextEnd || currentChar == '.' 
                          || currentChar == '!' 
                          || currentChar == '?')
            {
                CountNGrams(words, counts);
                words.Clear();
            }
        }

        foreach (var entry in counts)
        {
            var bestWord = string.Empty;
            var bestCount = -1;

            foreach (var continuation in entry.Value)
            {
                if (continuation.Value > bestCount 
                    || (continuation.Value == bestCount
                        && string.CompareOrdinal(continuation.Key, bestWord) < 0))
                {
                    bestWord = continuation.Key;
                    bestCount = continuation.Value;
                }
            }

            result[entry.Key] = bestWord;
        }

        return result;
    }

    private static void CountNGrams(
        List<string> words,
        Dictionary<string, Dictionary<string, int>> counts)
    {
        for (var i = 0; i + 1 < words.Count; i++)
        {
            CountContinuation(words[i], words[i + 1], counts);

            if (i + 2 < words.Count)
            {
                var prefix = words[i] + " " + words[i + 1];
                CountContinuation(prefix, words[i + 2], counts);
            }
        }
    }

    private static void CountContinuation(
        string prefix,
        string continuation,
        Dictionary<string, Dictionary<string, int>> counts)
    {
        if (!counts.TryGetValue(prefix, out var continuations))
        {
            continuations = new Dictionary<string, int>(
                StringComparer.Ordinal);
            counts[prefix] = continuations;
        }

        continuations.TryGetValue(continuation, out var count);
        continuations[continuation] = count + 1;
    }
}
