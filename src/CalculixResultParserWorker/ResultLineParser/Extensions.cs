namespace CalculixResultParserWorker.ResultLineParser;

public static class Extensions
{
    public static bool DisplacementSegment(this string line) => line.StartsWith(" -4") && line.Contains("DISP");
    public static bool StressSegment(this string line) => line.StartsWith(" -4") && line.Contains("STRESS");
    public static bool EndSegment(this string line) => line.StartsWith(" -3");
    public static bool ResultLine(this string line) => line.StartsWith(" -1");
}
