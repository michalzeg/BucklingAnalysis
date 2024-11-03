using CalculationActivities;
using CalculationActivities.Activities;
using CalculationActivities.ActivityArguments;
using CalculixSolverWorker.Services;
using Shared;


namespace CalculixSolverWorker
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly ISolver _solver;

        public ActivityHandler(ISolver solver)
        {
            _solver = solver;
        }

        public async Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument)
        {
            var analysisType = context.ActivityName switch
            {
                nameof(RunLinearAnalysisActivity) => AnalysisType.Linear,
                nameof(RunBucklingAnalysisActivity) => AnalysisType.Buckling,
                nameof(RunNonLinearAnalysisActivity) => AnalysisType.Nonlinear,
                _ => throw new ArgumentOutOfRangeException(nameof(context.ActivityName))
            };

            await _solver.Run(analysisType, context.TrackingNumber);

            return argument;
        }
    }
}
