using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CalculationActivities.Activities;

public class GenerateStructureActivity : ActivityBase
{
    public GenerateStructureActivity(ILogger<TriangulateActivity> logger, IActivityHandler activityHandler)
        : base(logger, activityHandler)
    {
    }
}
