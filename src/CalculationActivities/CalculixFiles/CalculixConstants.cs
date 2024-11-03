using Microsoft.Extensions.Configuration;
using Shared;

namespace CalculationActivities.CalculixFiles;

public static class CalculixConstants
{
    private const string CalculixWorkingDirectory = nameof(CalculixWorkingDirectory);
    public static string GetCalculixDirectory(this IConfiguration configuration) => configuration[CalculixWorkingDirectory]
        ?? throw new ArgumentNullException(CalculixWorkingDirectory);

    public static string GetCalculixPath(this IConfiguration configuration, Guid trackingNumber) =>
        Path.Combine(configuration.GetCalculixDirectory(), trackingNumber.ToString());

    public static string GetInputPath(this IConfiguration configuration, Guid trackingNumber, AnalysisType analysisType) =>
        Path.Combine(configuration.GetCalculixPath(trackingNumber), $"{analysisType}{InputFileExtension}");

    public static string GetResultPath(this IConfiguration configuration, Guid trackingNumber, AnalysisType analysisType) =>
        Path.Combine(configuration.GetCalculixPath(trackingNumber), $"{analysisType}{ResultFileExtension}");

    public const string InputFileExtension = ".inp";
    public const string ResultFileExtension = ".frd";
}
