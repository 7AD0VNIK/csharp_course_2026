using System.Globalization;
using System.Text;

namespace App.Lab2;

public class LogAggregator
{
    private const int TimestampStart = 1;
    private const int TimestampLength = 19;
    private const int TimestampEnd = 20;
    private const int TraceStart = 23;
    private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss";

    public static string MergeLogs(params string[] logLines)
    {
        if (logLines == null || logLines.Length == 0)
        {
            return string.Empty;
        }

        var logsByTraceId =
            new Dictionary<string, List<(DateTime Timestamp, string ServiceName, string FormattedLine)>>(
                StringComparer.Ordinal);

        foreach (var line in logLines)
        {
            if (!TryParseLog(line, out var traceId, out var log))
            {
                continue;
            }

            if (!logsByTraceId.TryGetValue(traceId, out var logs))
            {
                logs = new List<(DateTime Timestamp, string ServiceName, string FormattedLine)>();
                logsByTraceId[traceId] = logs;
            }

            logs.Add(log);
        }

        if (logsByTraceId.Count == 0)
        {
            return string.Empty;
        }

        foreach (var logs in logsByTraceId.Values)
        {
            InsertionSortLogs(logs);
        }

        var traceIds = new List<string>(logsByTraceId.Keys);
        SortTraceIds(traceIds, logsByTraceId);
        var result = new StringBuilder();

        for (var i = 0; i < traceIds.Count; i++)
        {
            if (i > 0)
            {
                result.AppendLine();
                result.AppendLine();
            }

            var traceId = traceIds[i];

            result
                .Append("--- TraceId: [")
                .Append(traceId)
                .Append("] ---");

            foreach (var log in logsByTraceId[traceId])
            {
                result.AppendLine();
                result.Append(log.FormattedLine);
            }
        }

        return result.ToString();
    }

    private static bool TryParseLog(string line, out string traceId,
        out (DateTime Timestamp, string ServiceName, string FormattedLine) log)
    {
        traceId = string.Empty;
        log = default;

        if (string.IsNullOrEmpty(line) || line.Length <= TraceStart)
        {
            return false;
        }

        if (line[0] != '['
            || line[TimestampEnd] != ']'
            || line[TimestampEnd + 1] != ' '
            || line[TimestampEnd + 2] != '[')
        {
            return false;
        }

        var timestampText = line.Substring(TimestampStart, TimestampLength);

        if (!TryParseTimestamp(timestampText, out var timestamp))
        {
            return false;
        }

        var traceEnd = line.IndexOf(']', TraceStart);

        if (traceEnd == TraceStart
            || traceEnd < 0
            || traceEnd + 1 >= line.Length
            || line[traceEnd + 1] != ' ')
        {
            return false;
        }

        traceId = line.Substring(TraceStart, traceEnd - TraceStart);

        foreach (var character in traceId)
        {
            if (char.IsWhiteSpace(character) || character == '[')
            {
                return false;
            }
        }

        var serviceStart = traceEnd + 2;
        var separatorIndex = line.IndexOf(": ", serviceStart, StringComparison.Ordinal);

        if (separatorIndex <= serviceStart)
        {
            return false;
        }

        var serviceName = line.Substring(serviceStart, separatorIndex - serviceStart);

        foreach (var character in serviceName)
        {
            if (char.IsWhiteSpace(character)
                || character == ':'
                || character == '['
                || character == ']')
            {
                return false;
            }
        }

        var message = line.Substring(separatorIndex + 2);

        if (message.Contains('\r') || message.Contains('\n'))
        {
            return false;
        }

        var formattedLine = $"[{timestampText}] {serviceName}: {message}";
        log = (timestamp, serviceName, formattedLine);
        return true;
    }

    private static bool TryParseTimestamp(string timestampText, out DateTime timestamp)
    {
        return DateTime.TryParseExact(timestampText, TimestampFormat, CultureInfo.InvariantCulture, 
            DateTimeStyles.None, out timestamp);
    }
    
    // <0 : first раньше должен быть
    // >0 : first позже должен быть
    // =0 : эквивалентны
    private static int CompareLogs(
        (DateTime Timestamp, string ServiceName, string FormattedLine) first,
        (DateTime Timestamp, string ServiceName, string FormattedLine) second)
    {
        var comparison = first.Timestamp.CompareTo(second.Timestamp);

        if (comparison != 0)
        {
            return comparison;
        }

        comparison = string.CompareOrdinal(first.ServiceName, second.ServiceName);

        if (comparison != 0)
        {
            return comparison;
        }

        return string.CompareOrdinal(first.FormattedLine, second.FormattedLine);
    }

    private static void InsertionSortLogs(
        List<(DateTime Timestamp, string ServiceName, string FormattedLine)> logs)
    {
        for (var i = 1; i < logs.Count; i++)
        {
            var currentLog = logs[i];
            var j = i - 1;

            while (j >= 0 && CompareLogs(logs[j], currentLog) > 0)
            {
                logs[j + 1] = logs[j];
                j--;
            }

            logs[j + 1] = currentLog;
        }
    }

    private static int CompareTraceIds(string first, string second,
        Dictionary<string, List<(DateTime Timestamp, string ServiceName, string FormattedLine)>> logsByTraceId)
    {
        var firstTimestamp = logsByTraceId[first][0].Timestamp;
        var secondTimestamp = logsByTraceId[second][0].Timestamp;
        var comparison = firstTimestamp.CompareTo(secondTimestamp);

        if (comparison != 0)
        {
            return comparison;
        }

        return string.CompareOrdinal(first, second);
    }

    private static void SortTraceIds(
        List<string> traceIds,
        Dictionary<string, List<(DateTime Timestamp, string ServiceName, string FormattedLine)>> logsByTraceId)
    {
        for (var i = 1; i < traceIds.Count; i++)
        {
            var currentTraceId = traceIds[i];
            var j = i - 1;

            while (j >= 0 && CompareTraceIds(traceIds[j], currentTraceId, logsByTraceId) > 0)
            {
                traceIds[j + 1] = traceIds[j];
                j--;
            }

            traceIds[j + 1] = currentTraceId;
        }
    }
}
