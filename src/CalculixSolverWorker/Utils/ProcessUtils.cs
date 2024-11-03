using System.Diagnostics;

namespace CalculixSolverWorker.Utils;

public static class ProcessUtils
{
    public static Process Start(string fileName, string arguments)
    {
        var startInfoDos = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        return new Process { StartInfo = startInfoDos };
    }
}
