using System.Globalization;

namespace Shared.Extensions;

public static class DoubleExtensionMethods
{
    public const double MaximumDifferenceAllowed = 0.0000001;
    public static bool IsApproximatelyEqualTo(this double initialValue, double value) => initialValue.IsApproximatelyEqualTo(value, MaximumDifferenceAllowed);
    public static bool IsApproximatelyEqualTo(this double initialValue, double value, double maximumDifferenceAllowed) => Math.Abs(initialValue - value) < maximumDifferenceAllowed;
    public static bool IsApproximatelyEqualToZero(this double initialValue) => initialValue.IsApproximatelyEqualTo(0d);
    public static bool IsApproximatelyLessOrEqualTo(this double initialValue, double value, double maximumDifferenceAllowed = MaximumDifferenceAllowed) => initialValue < value || initialValue.IsApproximatelyEqualTo(value, maximumDifferenceAllowed);
    public static int CalculateHashCode(this double initialValue) => (int)(initialValue * 1000).Round();
    public static bool IsApproximatelyGreaterOrEqualTo(this double initialValue, double value, double maximumDifferenceAllowed = MaximumDifferenceAllowed) => initialValue > value || initialValue.IsApproximatelyEqualTo(value, maximumDifferenceAllowed);
    public static double Round(this double initialValue) => initialValue.Round(2);
    public static double Round(this double initialValue, int numberOfDigits) => Math.Round(initialValue, numberOfDigits);
    public static bool IsNaN(this double initialValue) => double.IsNaN(initialValue);
    public static double Power(this double @base, double exponent) => Math.Pow(@base, exponent);
    public static string ToDotDecimal(this double value) => value.ToString("0.00", CultureInfo.InvariantCulture);
}