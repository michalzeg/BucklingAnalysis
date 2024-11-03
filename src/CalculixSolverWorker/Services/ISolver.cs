using Shared;

namespace CalculixSolverWorker.Services
{
    public interface ISolver
    {
        Task Run(AnalysisType analysisType, Guid trackingNumber);
    }
}
