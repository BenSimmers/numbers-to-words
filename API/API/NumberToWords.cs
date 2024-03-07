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
    public string? NumberToWordsFunc(string number)
    {
        try
        {
            string[] parts = number.Split('.', ',', ' ');
            if (parts[0] == "") parts[0] = "0";

            BigInteger dollars = BigInteger.Parse(parts[0]);
            string centsStr =
                parts.Length > 1 ? parts[1].PadRight(2, '0') : "00"; // Ensure cents part is always two digits
            BigInteger cents = BigInteger.Parse(centsStr.Substring(0, 2)); // Take only the first two digits of cents


            if (centsStr.Length > 2 && int.Parse(centsStr.Substring(2, 1)) >= 5)
                cents++; // Round cents if more than two digits and the third digit is greater than or equal to 5


            string? dollarsWords = ConvertToWords(dollars, dollars == 1 ? "DOLLAR" : "DOLLARS");
            string? centsWords = ConvertToWords(cents, "CENTS");

            return dollars == 0 ? centsWords : cents == 0 ? dollarsWords : $"{dollarsWords} AND {centsWords}";
        }
        catch
        {
            return null;
        }
    }


    /// <summary>
    /// This method will convert the number to words and return the result as string
    /// </summary>
    /// <param name="num"></param>
    /// <param name="unit"></param>
    /// <returns name="result"></returns>
    private string? ConvertToWords(BigInteger num, string unit)
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
        return num switch // shorthand switch statement
        {
            0 => "", // 0
            < 10 => _ones[num], // 1 to 9
            < 20 => _teens[num - 10], // 10 to 19
            < 100 => _tens[num / 10] + (num % 10 != 0 ? "-" + _ones[num % 10] : ""), // 20 to 99
            _ => $"{_ones[num / 100]} HUNDRED" +
                 (num % 100 != 0 ? " AND " + ConvertUnderThousand(num % 100) : "") // 100 to 999
        };
    }
}