using CalculixInputGeneratorWorker.Extensions;
using Shared.Contracts.Structure;
using System.Text;

namespace CalculixInputGeneratorWorker.CalculixInpuGenerator;


public abstract class CalculixInputGeneratorBase : ICalculixInputGenerator
{
    protected const string ELSET_ALL_ELEMENTS = nameof(ELSET_ALL_ELEMENTS);
    protected const string MATERIAL_NAME = nameof(MATERIAL_NAME);
    protected const string NSET_LOADS_FX = nameof(NSET_LOADS_FX);
    protected const string NSET_LOADS_FY = nameof(NSET_LOADS_FY);
    protected const string NSET_LOADS_FZ = nameof(NSET_LOADS_FZ);

    protected const string NSET_SUPPORT_UX = nameof(NSET_SUPPORT_UX);
    protected const string NSET_SUPPORT_UY = nameof(NSET_SUPPORT_UY);
    protected const string NSET_SUPPORT_UZ = nameof(NSET_SUPPORT_UZ);

    protected const string NSET_ALL = nameof(NSET_ALL);

    public async Task Run(StructureDetails structure, string fileName)
    {

        var col = Enumerable.Empty<string>()
            .Concat(GetHeader())
            .Concat(GetNodes(structure))
            .Concat(GetElements(structure))
            .Concat(GetMaterials(structure))
            .Concat(GetNodeSets(structure))
            .Concat(GetLoadSets(structure))
            .Concat(GetNodeSupports(structure))
            .Concat(GetStep(structure))
            ;

        var opt = new FileStreamOptions()
        {
            Access = FileAccess.Write,
        };

        await File.WriteAllTextAsync(fileName, string.Empty);
        await using var stream = new StreamWriter(fileName, Encoding.UTF8, opt);
        foreach (var item in col)
        {
            await stream.WriteLineAsync(item);
        }

    }

    protected abstract IEnumerable<string> GetStep(StructureDetails structure);

    private static IEnumerable<string> GetHeader()
    {
        yield return "*HEADING";
        yield return $"Model: ccx Date: {DateTime.Now}";
    }

    private static IEnumerable<string> GetNodes(StructureDetails structure)
    {
        yield return "*NODE";
        foreach (var node in structure.Elements.SelectMany(e => e.Nodes).Distinct())
        {
            yield return node.NormalizeToCalculix();
        }
    }

    private static IEnumerable<string> GetElements(StructureDetails structure)
    {
        foreach (var element in structure.Elements)
        {
            yield return $"*ELEMENT, TYPE={element.ElementType}, ELSET={ELSET_ALL_ELEMENTS}";
            yield return element.NormalizeToCalculix();
        }
    }

    private static IEnumerable<string> GetMaterials(StructureDetails structure)
    {
        yield return $"*MATERIAL, NAME={MATERIAL_NAME}";
        yield return "*ELASTIC";
        yield return "210000000000, 0.3";
        yield return "*DENSITY";
        yield return "78";
        yield return $"*SOLID SECTION, ELSET={ELSET_ALL_ELEMENTS},MATERIAL={MATERIAL_NAME}";
    }

    private static IEnumerable<string> GetNodeSets(StructureDetails structure)
    {
        var nodeCount = structure.Elements.SelectMany(e => e.Nodes).Distinct().Count();
        yield return $"*NSET, NSET={NSET_ALL}, GENERATE";
        yield return $"1,{nodeCount}";

    }
    private static IEnumerable<string> GetLoadSets(StructureDetails structure)
    {
        foreach (var item in structure.Loads.Where(e => e.FX != default).Select((load, index) => (load, index)))
        {
            yield return $"*NSET, NSET={NSET_LOADS_FX}_{item.index}";
            yield return item.load.Id.Value.NormalizeToCalculix();
        }

        foreach (var item in structure.Loads.Where(e => e.FY != default).Select((load, index) => (load, index)))
        {
            yield return $"*NSET, NSET={NSET_LOADS_FY}_{item.index}";
            yield return item.load.Id.Value.NormalizeToCalculix();
        }

        foreach (var item in structure.Loads.Where(e => e.FZ != default).Select((load, index) => (load, index)))
        {
            yield return $"*NSET, NSET={NSET_LOADS_FZ}_{item.index}";
            yield return item.load.Id.Value.NormalizeToCalculix();
        }

        yield return string.Empty;
    }

    private static IEnumerable<string> GetNodeSupports(StructureDetails structure)
    {
        var ux = structure.Supports.UX.Select(e => e.Value).NormalizeToCalculix();
        var uy = structure.Supports.UY.Select(e => e.Value).NormalizeToCalculix();
        var uz = structure.Supports.UZ.Select(e => e.Value).NormalizeToCalculix();

        yield return $"*NSET, NSET={NSET_SUPPORT_UX}";
        yield return ux;

        yield return $"*NSET, NSET={NSET_SUPPORT_UY}";
        yield return uy;

        yield return $"*NSET, NSET={NSET_SUPPORT_UZ}";
        yield return uz;
    }

    protected static IEnumerable<string> GetStepBoundaries()
    {
        yield return $"*BOUNDARY, op=New";

        yield return $"*BOUNDARY";
        yield return $"{NSET_SUPPORT_UX}, 1, 1, 0";

        yield return $"*BOUNDARY";
        yield return $"{NSET_SUPPORT_UY}, 2, 2, 0";

        yield return $"*BOUNDARY";
        yield return $"{NSET_SUPPORT_UZ}, 3, 3, 0";
    }

    protected static IEnumerable<string> GetStepLoads(StructureDetails structure)
    {
        foreach (var item in structure.Loads.Where(e => e.FX != default).Select((load, index) => (load, index)))
        {
            yield return $"*CLOAD";
            yield return $"{NSET_LOADS_FX}_{item.index},1,{item.load.FX}";
        }

        foreach (var item in structure.Loads.Where(e => e.FY != default).Select((load, index) => (load, index)))
        {
            yield return $"*CLOAD";
            yield return $"{NSET_LOADS_FY}_{item.index},2,{item.load.FY}";
        }

        foreach (var item in structure.Loads.Where(e => e.FZ != default).Select((load, index) => (load, index)))
        {
            yield return $"*CLOAD";
            yield return $"{NSET_LOADS_FZ}_{item.index},3,{item.load.FZ}";
        }
    }
}
