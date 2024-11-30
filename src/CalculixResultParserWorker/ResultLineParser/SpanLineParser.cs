using Shared.Contracts.Results;
using Shared.Contracts;
using System.Globalization;

namespace CalculixResultParserWorker.ResultLineParser;

public class SpanLineParser : IResultLineParser
{
    // example: " -1       156 5.73824E-01-2.19689E-02-2.35122E+01"
    public NodeDisplacementResult ParseNodeDisplacementResult(string line)
    {
        Span<char> input = stackalloc char[line.Length];
        line.CopyTo(input);

        //remove -1 from beginning
        input = input.Trim().TrimStart("-1").Trim();

        var nodeNumber = GetNodeNumber(ref input);

        Span<double> parsedNumbers = stackalloc double[3];
        ParseNumbers(input, ref parsedNumbers);

        return new NodeDisplacementResult()
        {
            Id = new NodeId(nodeNumber),
            Dx = parsedNumbers[0],
            Dy = parsedNumbers[1],
            Dz = parsedNumbers[2]
        };
    }
    // example:  -1       292-2.02385E+02-8.42151E+00-4.40704E+03 8.61601E+00-1.08939E+01-1.22923E+03
    public NodeStressResult ParseNodeStressResult(string line)
    {
        Span<char> input = stackalloc char[line.Length];
        line.CopyTo(input);

        //remove -1 from beginning
        input = input.Trim().TrimStart("-1").Trim();

        var nodeNumber = GetNodeNumber(ref input);

        Span<double> parsedNumbers = stackalloc double[6];
        ParseNumbers(input, ref parsedNumbers);

        var sxx = parsedNumbers[0];
        var syy = parsedNumbers[1];
        var szz = parsedNumbers[2];
        var sxy = parsedNumbers[3];
        var sxz = parsedNumbers[4];
        var syz = parsedNumbers[5];

        return new NodeStressResult()
        {
            Id = new NodeId(nodeNumber),
            Sxx = sxx,
            Syy = syy,
            Szz = szz,
            Sxy = sxy,
            Sxz = sxz,
            Syz = syz,
        };
    }

    private static void ParseNumbers(Span<char> input, ref Span<double> parsedNumbers)
    {
        parsedNumbers.Fill(0d);
        Span<char> number = stackalloc char[input.Length];
        number.Fill(' ');
        int numberIndex = 0;
        int index = 0;
        var isExponent = false;
        var exponentSign = false;

        for (int i = 0; i < input.Length; i++)
        {
            var currentChar = input[i];
            if (currentChar == 'E')
            {
                isExponent = true;
            }

            else if (isExponent && exponentSign && (currentChar == ' ' || currentChar == '-' || i == input.Length - 1))
            {
                if (i == input.Length - 1)
                {
                    number[index] = currentChar;
                }

                //new number
                parsedNumbers[numberIndex] = double.Parse(number, CultureInfo.InvariantCulture);
                numberIndex++;
                number.Fill(' ');
                index = 0;
                isExponent = false;
                exponentSign = false;
                if (currentChar == '-')
                {
                    number[index] = currentChar;
                    index++;
                }
                continue;
            }
            else if (isExponent && (currentChar == '-' || currentChar == '+'))
            {
                exponentSign = true;
            }
            number[index] = currentChar;
            index++;
        }
    }

    private static int GetNodeNumber(ref Span<char> input)
    {
        Span<char> number = stackalloc char[input.Length];
        number.Fill(' ');
        var index = 0;
        while (input[index] != ' ' && input[index] != '-')
        {
            number[index] = input[index];
            index++;
        }
        input = input[(index)..].Trim();
        var nodeNumber = int.Parse(number, CultureInfo.InvariantCulture);
        return nodeNumber;
    }
}
