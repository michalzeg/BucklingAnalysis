using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class GenerateFacadeActivity : ActivityBase
{
    public GenerateFacadeActivity(ILogger<TriangulateActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
