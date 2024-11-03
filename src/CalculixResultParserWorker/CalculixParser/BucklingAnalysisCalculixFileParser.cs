using CalculixResultParserWorker.ResultLineParser;
using Shared.Contracts.Results;

namespace CalculixResultParserWorker.CalculixParser;

public class BucklingAnalysisCalculixFileParser : ICalculixFileParser
{
    private readonly IResultLineParser _resultLineParser;

    public BucklingAnalysisCalculixFileParser(IResultLineParser resultLineParser)
    {
        _resultLineParser = resultLineParser;
    }

    public async Task<NodeResults> Parse(string file)
    {
        var displacements = new List<NodeDisplacementResult>();

        var parsingDisplacement = false;
        var firstResult = false; //ignore first results as it does not contain buckling shape
        string? line;

        await using FileStream fileStream = new(file, FileMode.Open, FileAccess.Read);
        using StreamReader reader = new(fileStream);

        while ((line = await reader.ReadLineAsync()) is not null)
        {
            if (line.DisplacementSegment() && !firstResult)
            {
                firstResult = true;
            }
            else if (line.DisplacementSegment() && firstResult)
            {
                parsingDisplacement = true;
            }
            else if (line.EndSegment())
            {
                parsingDisplacement = false;
            }

            if (line.ResultLine() && parsingDisplacement)
            {
                var displacement = _resultLineParser.ParseNodeDisplacementResult(line);
                displacements.Add(displacement);
            }
        }
        return new NodeResults()
        {
            Displacements = displacements,
            Stresses = []
        };
    }
}
