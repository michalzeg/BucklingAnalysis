namespace CalculixSolverWorker.Utils;

public static class ConfigurationUtils
{
    private const string Delay = nameof(Delay);
    public static TimeSpan GetDelay(this IConfiguration configuration)
    {
        var configValue = configuration.GetValue<string>(Delay);

        if (!int.TryParse(configValue, out var delay))
        {
            throw new ArgumentException($"Provided {delay} is in wrong format");
        }
        var result = TimeSpan.FromMilliseconds(delay);
        return result;
    }
}
