using Shared.Contracts.Structure;

namespace CalculixInputGeneratorWorker.CalculixInpuGenerator;

public interface ICalculixInputGenerator
{
    Task Run(StructureDetails structure, string fileName);
}
