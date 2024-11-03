using Shared.Contracts.Results;

namespace CalculixResultParserWorker.CalculixParser;

public interface ICalculixFileParser
{
    Task<NodeResults> Parse(string file);
}
