using CalculationActivities.ActivityArguments;

namespace CalculationActivities;
public interface IActivityHandler
{
    Task<ActivityArgument> Handle(ActivityContext context, ActivityArgument argument);
}
