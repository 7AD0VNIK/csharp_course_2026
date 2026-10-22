
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

        var logsByTraceId = new Dictionary<string, List<string>>(StringComparer.Ordinal);

        foreach (var line in logLines)
        {
            if (!TryParseLog(line, out var traceId, out var formattedLine))
            {
                continue;
            }

            if (!logsByTraceId.TryGetValue(traceId, out var logs))
            {
                logs = new List<string>();
                logsByTraceId[traceId] = logs;
            }

            logs.Add(formattedLine);
        }

        if (logsByTraceId.Count == 0)
        {
            return string.Empty;
        }

        foreach (var logs in logsByTraceId.Values)
        {
            SortLogs(logs);
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

            result.Append("--- TraceId: [")
                .Append(traceId)
                .Append("] ---");

            foreach (var log in logsByTraceId[traceId])
            {
                result.AppendLine();
                result.Append(log);
            }
        }

        return result.ToString();
    }

    private static bool TryParseLog(
        string line,
        out string traceId,
        out string formattedLine)
    {
        traceId = string.Empty;
        formattedLine = string.Empty;

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

        var timestamp = line.Substring(
            TimestampStart, TimestampLength);

        if (!DateTime.TryParseExact(
                timestamp,
                TimestampFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
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

        traceId = line.Substring(
            TraceStart, traceEnd - TraceStart);

        foreach (var character in traceId)
        {
            if (char.IsWhiteSpace(character)
                || character == '[')
            {
                return false;
            }
        }

        var serviceStart = traceEnd + 2;
        var separatorIndex = line.IndexOf(
            ": ", serviceStart, StringComparison.Ordinal);

        if (separatorIndex <= serviceStart)
        {
            return false;
        }

        var serviceName = line.Substring(
            serviceStart, separatorIndex - serviceStart);

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

        formattedLine = $"[{timestamp}] {serviceName}: {message}";
        return true;
    }

    private static string GetTimestamp(string log)
    {
        return log.Substring(TimestampStart, TimestampLength);
    }

    private static string GetServiceName(string log)
    {
        var serviceStart = TimestampEnd + 2;
        var separatorIndex = log.IndexOf(": ", serviceStart, StringComparison.Ordinal);

        return log.Substring(serviceStart, separatorIndex - serviceStart);
    }

    private static int CompareLogs(string first, string second)
    {
        var comparison = string.CompareOrdinal(GetTimestamp(first), GetTimestamp(second));

        if (comparison != 0)
        {
            return comparison;
        }

        comparison = string.CompareOrdinal(GetServiceName(first), GetServiceName(second));

        if (comparison != 0)
        {
            return comparison;
        }

        return string.CompareOrdinal(first, second);
    }

    private static void SortLogs(List<string> logs)
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

    private static int CompareTraceIds(
        string first,
        string second,
        Dictionary<string, List<string>> logsByTraceId)
    {
        var comparison = string.CompareOrdinal(
            GetTimestamp(logsByTraceId[first][0]),
            GetTimestamp(logsByTraceId[second][0]));

        if (comparison != 0)
        {
            return comparison;
        }

        return string.CompareOrdinal(first, second);
    }

    private static void SortTraceIds(
        List<string> traceIds,
        Dictionary<string, List<string>> logsByTraceId)
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
