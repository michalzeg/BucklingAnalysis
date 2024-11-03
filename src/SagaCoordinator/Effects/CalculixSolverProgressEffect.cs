using MassTransit;
using SagaCoordinator.Saga;
using Shared.Events;
using Shared.Notifications;
using System.Text.RegularExpressions;

namespace SagaCoordinator.Effects;

public class CalculixSolverProgressEffect : IEffect<CalculixSolverProgress>
{
    const string _pattern = @"iteration=\s*(\d+),\s*error=\s*([\d.eE+-]+),\s*limit=\s*([\d.eE+-]+)";
    private static readonly Regex _regex = new Regex(_pattern, RegexOptions.Compiled);
    private readonly ILogger<CalculixSolverProgressEffect> _logger;

    public CalculixSolverProgressEffect(ILogger<CalculixSolverProgressEffect> logger)
    {
        _logger = logger;
    }

    public void Then(BehaviorContext<CalculationsCoordinatorState, CalculixSolverProgress> context)
    {
    }

    public async Task ThenAsync(BehaviorContext<CalculationsCoordinatorState, CalculixSolverProgress> context)
    {
        
        // Perform the match
        var match = _regex.Match(context.Message.ProgressLine);

        if (match.Success)
        {
            // Parse the matched values
            int iteration = int.Parse(match.Groups[1].Value);
            double error = double.Parse(match.Groups[2].Value);
            double limit = double.Parse(match.Groups[3].Value);

            var progress = new CalculixProgressValue(iteration, error, limit);

            await context.Publish<CalculixProgressNotification>(new(context.Message.TrackingNumber, progress));
        }
    }
}
