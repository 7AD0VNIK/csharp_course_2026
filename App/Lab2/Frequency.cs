using System.Text;

namespace App.Lab2;

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
        var wordBuilder = new StringBuilder();

        var sentences = inputString.Split(
            new[] { '.', '!', '?' },
            StringSplitOptions.RemoveEmptyEntries);

        foreach (var sentence in sentences)
        {
            foreach (var currentChar in sentence)
            {
                if (char.IsLetterOrDigit(currentChar))
                {
                    wordBuilder.Append(char.ToLowerInvariant(currentChar));
                    continue;
                }

                if (wordBuilder.Length > 0)
                {
                    words.Add(wordBuilder.ToString());
                    wordBuilder.Clear();
                }
            }

            if (wordBuilder.Length > 0)
            {
                words.Add(wordBuilder.ToString());
                wordBuilder.Clear();
            }

            var sentenceWords = words.ToArray();
            CountNGrams(sentenceWords, 2, counts);
            CountNGrams(sentenceWords, 3, counts);
            words.Clear();
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

    private static void CountNGrams(string[] words, int n, Dictionary<string, Dictionary<string, int>> counts)
    {
        for (var i = 0; i <= words.Length - n; i++)
        {
            var prefix = string.Join(" ", words, i, n - 1);
            var continuation = words[i + n - 1];

            CountContinuation(prefix, continuation, counts);
        }
    }

    private static void CountContinuation(
        string prefix,
        string continuation,
        Dictionary<string, Dictionary<string, int>> counts)
    {
        if (!counts.TryGetValue(prefix, out var continuations))
        {
            continuations = new Dictionary<string, int>(StringComparer.Ordinal);
            counts[prefix] = continuations;
        }

        continuations.TryGetValue(continuation, out var count);
        continuations[continuation] = count + 1;
    }
}