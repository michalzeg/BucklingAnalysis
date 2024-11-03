using CalculationActivities.CalculixFiles;
using CalculixSolverWorker.Utils;
using Shared;

namespace CalculixSolverWorker.Services
{

    public class Solver : ISolver
    {
        private readonly ILogger<Solver> _logger;
        private readonly INotificationService _notificationService;
        private readonly ICalculixFileManager _calculixFileManager;

        public Solver(ILogger<Solver> logger, INotificationService notificationService, ICalculixFileManager calculixFileManager)
        {
            _logger = logger;
            _notificationService = notificationService;
            _calculixFileManager = calculixFileManager;
        }
        public async Task Run(AnalysisType analysisType, Guid trackingNumber)
        {

            var filePath = _calculixFileManager.GetInputFileName(trackingNumber, analysisType);
            var model = _calculixFileManager.GetModelName(trackingNumber, analysisType);

            await Dos2Unix(filePath);

            using var process = ProcessUtils.Start("ccx", model);
            process.OutputDataReceived += (sender, e) => _notificationService.SendProgress(trackingNumber, e.Data);

            process.ErrorDataReceived += (sender, e) => _logger.LogError("Error: {Data}", e.Data);

            // Start the process
            process.Start();

            // Begin asynchronously reading the output
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // Wait for the process to exit
            await process.WaitForExitAsync();
        }

        private static async Task Dos2Unix(string path)
        {
            using var process = ProcessUtils.Start("dos2unix", path);
            process.Start();
            await process.WaitForExitAsync();
        }
    }
}
