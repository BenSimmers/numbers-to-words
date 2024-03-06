using System.Numerics;

namespace API;

/// <summary>
/// This class will convert the number to words
/// </summary>
public class NumbersToWords
{
    private readonly string[] _ones =
        { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };

    private readonly string[] _teens =
    {
        "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
    };

    private readonly string[] _tens =
        { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

    private readonly string[] _thousands =
    {
        "", "THOUSAND", "MILLION", "BILLION", "TRILLION", "QUADRILLION", "QUINTILLION", "SEXTILLION", "SEPTILLION",
        "OCTILLION", "NONILLION", "DECILLION"
    };

    /// <summary>
    /// We pass in the number as a string and split
    /// it into dollars and cents to convert to words
    /// </summary>
    /// <param name="number"></param>
    /// <returns name="result"></returns>
    public string NumberToWordsFunc(string number)
    {
        string[] parts = number.Split('.');
        BigInteger dollars = BigInteger.Parse(parts[0]);

        if (parts.Length == 1 || string.IsNullOrWhiteSpace(parts[1])) // No cents or empty fractional part
        {
            if (dollars == 0) return "";
            return ConvertToWords(dollars, "DOLLARS");
        }

        BigInteger cents = BigInteger.Parse(parts[1]);

        string dollarsWords = ConvertToWords(dollars, "DOLLARS");
        string centsWords = ConvertToWords(cents, "CENTS");

        if (dollars == 0) return $"{centsWords}";
        else
        {
            if (cents == 0)
            {
                return $"{dollarsWords}";
            }
            else
            {
                return $"{dollarsWords} AND {centsWords}";
            }
        }
    }


    /// <summary>
    /// This method will convert the number to words and return the result as string
    /// </summary>
    /// <param name="num"></param>
    /// <param name="unit"></param>
    /// <returns name="result"></returns>
    private string ConvertToWords(BigInteger num, string unit)
    {
        string result = "";
        if (num == 0) return "";

        int magnitude = 0;

        while (num > 0)
        {
            if (num % 1000 != 0)
            {
                string groupWords = ConvertUnderThousand((int)(num % 1000));
                if (!string.IsNullOrEmpty(groupWords))
                {
                    string magWord = GetMagnitudeWord(magnitude);
                    result = $"{groupWords} {magWord} {result}";
                }
            }

            num /= 1000;
            magnitude += 3; // Move to the next magnitude (thousands, millions, etc.)
        }

        return $"{result.Trim()} {unit}";
    }

    /// <summary>
    /// We want to dynmically get the magnitude word for the number when it passed the max value
    /// in the _thousands array
    /// </summary>
    /// <param name="magnitude"></param>
    /// <returns></returns>
    private string GetMagnitudeWord(int magnitude)
    {
        string[] magnitudePrefixesAboveDec =
            { "VIGINT", "TRIGINT", "QUATTUORDEC", "QUINDEC", "SEXDEC", "SEPTENDEC", "OCTODEC", "NOVEMDEC", "VIGINT" };

        if (magnitude < 12) return _thousands[magnitude / 3];
        if (magnitude % 6 == 0) return magnitudePrefixesAboveDec[magnitude / 6 - 3] + "ILLION";
        return magnitudePrefixesAboveDec[magnitude / 6 - 3] + "ILLION";
    }

    /// <summary>
    /// We want to focus on the number less than 1000 and convert it to words
    /// because we are using the _thousands array to convert the number to words
    /// </summary>
    /// <param name="num"></param>
    /// <returns name="result"></returns>
    private string ConvertUnderThousand(int num)
    {
        // Convert a number less than 1000 to words
        string result;
        // 0 to 999
        switch (num)
        {
            case 0:
                return "";
            case < 10: // 1 to 9
                result = _ones[num]; // e.g. ONE, TWO, THREE, etc.
                break;
            case < 20: // 10 to 19
                result = _teens[num - 10]; // e.g. TEN, ELEVEN, TWELVE, etc.
                break;
            case < 100: // 20 to 99
                result = _tens[num / 10] +
                         (num % 10 != 0 ? "-" + _ones[num % 10] : ""); // e.g. TWENTY-ONE, THIRTY-TWO, etc.
                break;
            default: // 100 to 999
                result =
                    $"{_ones[num / 100]} HUNDRED {ConvertUnderThousand(num % 100)}"; // e.g. ONE HUNDRED TWENTY-THREE, etc.
                break;
        }

        return result.Trim();
    }
}