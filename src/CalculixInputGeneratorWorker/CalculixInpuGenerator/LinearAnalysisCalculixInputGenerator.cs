
using Shared.Contracts.Structure;

namespace CalculixInputGeneratorWorker.CalculixInpuGenerator;

public class LinearAnalysisCalculixInputGenerator : CalculixInputGeneratorBase
{
    public static LinearAnalysisCalculixInputGenerator Instance => new LinearAnalysisCalculixInputGenerator();

    protected override IEnumerable<string> GetStep(StructureDetails structure)
    {
        yield return $"*STEP";
        yield return $"*STATIC, Solver=Iterative Cholesky";

        foreach (var item in GetStepLoads(structure))
        {
            yield return item;
        }

        foreach (var item in GetStepBoundaries())
        {
            yield return item;
        }

        yield return $"*NODE PRINT,NSET={NSET_ALL}";
        yield return $"U";
        yield return $"*EL PRINT,ELSET={ELSET_ALL_ELEMENTS}";
        yield return $"S";
        yield return $"*NODE FILE";
        yield return $"U";
        yield return $"*EL FILE";
        yield return $"S";

        yield return $"*END STEP";
    }
}
