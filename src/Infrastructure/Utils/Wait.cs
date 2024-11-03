using System.Net.Sockets;

namespace Infrastructure.Utils;

public static class Wait
{
    public static async Task Execute()
    {
        var config = WaitConfig.CreateFromEnvironmentVariables();
        var startingTime = DateTimeOffset.UtcNow;

        while (startingTime + TimeSpan.FromSeconds(config.WaitHostTimeout) > DateTimeOffset.UtcNow)
        {
            var tasks = config.WaitHosts.Select(e => e.Split(':')).Select(e => IsServiceAvailable(e[0], e[1])).ToArray();

            var results = await Task.WhenAll(tasks);
            if (results.All(e => e))
            {
                return;
            }
            await Task.Delay(config.WaitSleepInterval * 1000);
        }
        throw new ApplicationException("Cannot connect to dependant services");
    }

    private static async Task<bool> IsServiceAvailable(string ipAddress, string port, int timeout = 3000)
    {
        try
        {
            using var tcpClient = new TcpClient();
            // Use ConnectAsync to connect with a timeout
            var connectTask = tcpClient.ConnectAsync(ipAddress, int.Parse(port));
            if (await Task.WhenAny(connectTask, Task.Delay(timeout)) == connectTask)
            {
                return tcpClient.Connected;
            }
            else
            {
                return false;
            }
        }
        catch (SocketException)
        {
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
}
