namespace Infrastructure.Utils;

public class WaitConfig
{
    private const string WAIT_HOSTS = nameof(WAIT_HOSTS);
    private const string WAIT_SLEEP_INTERVAL = nameof(WAIT_SLEEP_INTERVAL);
    private const string WAIT_HOSTS_TIMEOUT = nameof(WAIT_HOSTS_TIMEOUT);

    public static WaitConfig CreateFromEnvironmentVariables()
    {
        var waitHosts = Environment.GetEnvironmentVariable(WAIT_HOSTS)?.Split(',').Where(e => !string.IsNullOrWhiteSpace(e)).ToArray() ?? [];

        var result = new WaitConfig()
        {
            WaitHosts = waitHosts
        };

        var waitSleepIntervalEnv = Environment.GetEnvironmentVariable(WAIT_SLEEP_INTERVAL);
        if(int.TryParse(waitSleepIntervalEnv, out var waitSleepInterval)){
            result.WaitSleepInterval = waitSleepInterval;
        }

        var waitHostTimeoutEnv = Environment.GetEnvironmentVariable(WAIT_HOSTS_TIMEOUT);
        if (int.TryParse(waitHostTimeoutEnv, out var waitHostTimeout))
        {
            result.WaitHostTimeout = waitHostTimeout;
        }
        return result;
    }

    public required string[] WaitHosts { get; init; }
    public int WaitHostTimeout { get; set; } = 300;
    public int WaitSleepInterval { get; set; } = 10;
}
