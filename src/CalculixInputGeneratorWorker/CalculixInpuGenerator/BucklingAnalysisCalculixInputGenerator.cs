using Shared.Contracts.Structure;

namespace CalculixInputGeneratorWorker.CalculixInpuGenerator;

public class BucklingAnalysisCalculixInputGenerator : CalculixInputGeneratorBase
{
    public static BucklingAnalysisCalculixInputGenerator Instance => new BucklingAnalysisCalculixInputGenerator();

    protected override IEnumerable<string> GetStep(StructureDetails structure)
    {
        var buckingModes = 1;
        yield return $"*Step";
        yield return $"*Buckle, Solver=Spooles";
        yield return $"{buckingModes}, 0.01";

        yield return $"*Output, Frequency=1";

        foreach (var item in GetStepLoads(structure))
        {
            yield return item;
        }

        foreach (var item in GetStepBoundaries())
        {
            yield return item;
        }

        yield return $"*Node file";
        yield return $"U";

        yield return $"*End step";
    }
}
