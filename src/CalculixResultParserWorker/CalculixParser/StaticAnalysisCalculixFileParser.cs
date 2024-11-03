using CalculixResultParserWorker.ResultLineParser;
using Shared.Contracts.Results;

namespace CalculixResultParserWorker.CalculixParser;

public class StaticAnalysisCalculixFileParser : ICalculixFileParser
{
    private readonly IResultLineParser _resultLineParser;

    public StaticAnalysisCalculixFileParser(IResultLineParser resultLineParser)
    {
        _resultLineParser = resultLineParser;
    }

    public async Task<NodeResults> Parse(string file)
    {
        var displacements = new List<NodeDisplacementResult>();
        var stresses = new List<NodeStressResult>();

        var parsingDisplacement = false;
        var parsingStress = false;
        string? line;

        await using FileStream fileStream = new(file, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new(fileStream);

        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (line.DisplacementSegment())
            {
                parsingDisplacement = true;
            }
            else if (line.StressSegment())
            {
                parsingStress = true;
            }
            else if (line.EndSegment()) // end segment with displacement/stresses results
            {
                parsingDisplacement = false;
                parsingStress = false;
            }

            if (line.ResultLine() && parsingDisplacement)
            {
                var displacement = _resultLineParser.ParseNodeDisplacementResult(line);
                displacements.Add(displacement);
            }
            else if (line.ResultLine() && parsingStress)
            {
                var stress = _resultLineParser.ParseNodeStressResult(line);
                stresses.Add(stress);
            }

        }
        return new NodeResults()
        {
            Displacements = displacements,
            Stresses = stresses
        };
    }
}
