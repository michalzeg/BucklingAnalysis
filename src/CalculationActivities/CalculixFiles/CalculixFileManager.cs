using Microsoft.Extensions.Configuration;
using Shared;

namespace CalculationActivities.CalculixFiles;

public class CalculixFileManager : ICalculixFileManager
{
    private const string CalculixWorkingDirectory = nameof(CalculixWorkingDirectory);
    private const string InputFileExtension = ".inp";
    private const string ResultFileExtension = ".frd";


    private readonly IConfiguration _configuration;

    public CalculixFileManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetModelName(Guid trackingNumber, AnalysisType analysisType)
    {
        var dir = GetCalculixDirectory();
        var fileName = GetFileName(trackingNumber, analysisType);
        return Path.Combine(dir, fileName);
    }

    public string GetInputFileName(Guid trackingNumber, AnalysisType analysisType) => GetFullFileName(trackingNumber, analysisType, InputFileExtension);

    public string GetResultFileName(Guid trackingNumber, AnalysisType analysisType) => GetFullFileName(trackingNumber, analysisType, ResultFileExtension);

    private string GetFullFileName(Guid trackingNumber, AnalysisType analysisType, string extension)
    {
        var dir = GetCalculixDirectory();
        var fileName = GetFileName(trackingNumber, analysisType);
        var fullName = $"{fileName}{extension}";
        return Path.Combine(dir, fullName);
    }

    private string GetFileName(Guid trackingNumber, AnalysisType analysisType) => $"{trackingNumber}_{analysisType}";

    private string GetCalculixDirectory() => _configuration[CalculixWorkingDirectory]
        ?? throw new ArgumentNullException(CalculixWorkingDirectory);
}
