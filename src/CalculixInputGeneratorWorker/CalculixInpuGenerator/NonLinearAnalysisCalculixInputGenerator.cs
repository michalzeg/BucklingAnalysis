using Shared.Contracts.Structure;

namespace CalculixInputGeneratorWorker.CalculixInpuGenerator;

public class NonLinearAnalysisCalculixInputGenerator : CalculixInputGeneratorBase
{
    public static NonLinearAnalysisCalculixInputGenerator Instance => new NonLinearAnalysisCalculixInputGenerator();

    protected override IEnumerable<string> GetStep(StructureDetails structure)
    {
        yield return $"*STEP, Nlgeom, Inc=100";
        yield return $"*STATIC, Solver=Iterative Cholesky";
        yield return $"1, 1, 1E-05, 1E+30";
        yield return $"*Output, Frequency=1";


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
