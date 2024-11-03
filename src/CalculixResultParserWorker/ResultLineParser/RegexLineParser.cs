using Shared.Contracts;
using Shared.Contracts.Results;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CalculixResultParserWorker.ResultLineParser;

public class RegexLineParser : IResultLineParser
{
    private const string displacementPattern = @"(\d+)(\s?-?\d+\.\d+E[+-]?\d+)(\s?-?\d+\.\d+E[+-]?\d+)(\s?-?\d+\.\d+E[+-]?\d+)";
    private const string stressPattern = @"(\d+)(\s?-?\d+\.\d+E[+-]?\d+)(\s?-?\d+\.\d+E[+-]?\d+)(\s?-?\d+\.\d+E[+-]?\d+)(\s?-?\d+\.\d+E[+-]?\d+)(\s?-?\d+\.\d+E[+-]?\d+)(\s?-?\d+\.\d+E[+-]?\d+)";

    private static readonly Regex _displacementRegex = new(displacementPattern, RegexOptions.Compiled);
    private static readonly Regex _stressRegex = new(stressPattern, RegexOptions.Compiled);


    public NodeDisplacementResult ParseNodeDisplacementResult(string line)
    {
        var normalizedInput = line.Substring(2); //trim -1 at start
        var match = _displacementRegex.Match(normalizedInput);

        int number = int.Parse(match.Groups[1].Value); // Extract integer
        double double1 = double.Parse(match.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract first double
        double double2 = double.Parse(match.Groups[3].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract second double
        double double3 = double.Parse(match.Groups[4].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract third double

        return new NodeDisplacementResult()
        {
            Id = new NodeId(number),
            Dx = double1,
            Dy = double2,
            Dz = double3
        };
    }

    public NodeStressResult ParseNodeStressResult(string line)
    {
        var normalizedInput = line.Substring(2); //trim -1 at start
        var match = _stressRegex.Match(normalizedInput);

        var number = int.Parse(match.Groups[1].Value); // Extract integer
        var sxx = double.Parse(match.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract first double
        var syy = double.Parse(match.Groups[3].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract second double
        var szz = double.Parse(match.Groups[4].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract third double
        var sxy = double.Parse(match.Groups[5].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract third double
        var sxz = double.Parse(match.Groups[6].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract third double
        var syz = double.Parse(match.Groups[7].Value, NumberStyles.Float, CultureInfo.InvariantCulture); // Extract third double

        return new NodeStressResult()
        {
            Id = new NodeId(number),
            Sxx = sxx,
            Syy = syy,
            Szz = szz,
            Sxy = sxy,
            Sxz = sxz,
            Syz = syz,
        };
    }
}
