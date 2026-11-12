using System.Text;

namespace App.Lab2;

public class StackMachine
{
    public static string CalculateString(string[] codeLines)
    {
        var text = new StringBuilder();
        foreach (var line in codeLines)
        {
            if (line.StartsWith("push ", StringComparison.Ordinal))
            {
                text.Append(line.Substring(5));
                continue;
            }
            
            var count = int.Parse(line.Substring(4));
            text.Length -= count;
        }

        return text.ToString();
    }
}