using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class TriangulateActivity : ActivityBase
{
    public TriangulateActivity(ILogger<TriangulateActivity> logger , IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
