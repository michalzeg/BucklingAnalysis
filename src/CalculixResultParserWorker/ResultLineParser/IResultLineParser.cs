using Shared.Contracts.Results;

namespace CalculixResultParserWorker.ResultLineParser;

public interface IResultLineParser
{
    NodeStressResult ParseNodeStressResult(string line);
    NodeDisplacementResult ParseNodeDisplacementResult(string line);
}
