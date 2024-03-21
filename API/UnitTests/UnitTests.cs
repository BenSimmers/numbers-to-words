using API;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;

namespace UnitTests;

/// <summary>
/// This class will contain the tests for the NumbersToWords class (the main algorithm)
/// </summary>
public class Tests
{
    // Test for a valid number
    [Test]
    public void ValidNumber()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1234567890");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected =
            "ONE BILLION TWO HUNDRED AND THIRTY-FOUR MILLION FIVE HUNDRED AND SIXTY-SEVEN THOUSAND EIGHT HUNDRED AND NINETY DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for an invalid number
    [Test]
    public void InvalidNumber()
    {
        NumbersToWords numbersToWords = new NumbersToWords("Hello");
        string? words = numbersToWords.NumberToWordsFunc();

        // shoudl return error code 404 and the message "NOT FOUND: Please provide a valid number or number was out of range."
        Assert.That(words, Is.Null);
    }

    // Test for a valid number with cents
    [Test]
    public void FractionalNumber()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1234567890.12");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected =
            "ONE BILLION TWO HUNDRED AND THIRTY-FOUR MILLION FIVE HUNDRED AND SIXTY-SEVEN THOUSAND EIGHT HUNDRED AND NINETY DOLLARS AND TWELVE CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents
    [Test]
    public void FractionalNumberRounding()
    {
        NumbersToWords numbersToWords = new NumbersToWords("12.129");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE DOLLARS AND THIRTEEN CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents
    [Test]
    public void FractionalNumberRounding2()
    {
        NumbersToWords numbersToWords = new NumbersToWords("12.125");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE DOLLARS AND THIRTEEN CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents
    [Test]
    public void FrationalButNoCents()
    {
        NumbersToWords numbersToWords = new NumbersToWords("12.");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents
    [Test]
    public void FrationalButNoCents2()
    {
        NumbersToWords numbersToWords = new NumbersToWords("12.0");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents
    [Test]
    public void FrationalButNoCents3()
    {
        NumbersToWords numbersToWords = new NumbersToWords("12.00");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents
    [Test]
    public void FrationalButNoCents4()
    {
        NumbersToWords numbersToWords = new NumbersToWords("12.000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents but no dollars
    [Test]
    public void FractionalButNoDollars()
    {
        NumbersToWords numbersToWords = new NumbersToWords(".12");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents but no dollars
    [Test]
    public void FractionalButNoDollars2()
    {
        NumbersToWords numbersToWords = new NumbersToWords("0.12");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents but no dollars
    [Test]
    public void FractionalButNoDollars3()
    {
        NumbersToWords numbersToWords = new NumbersToWords("0.12");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents but no dollars
    [Test]
    public void FractionalButNoDollars4()
    {
        NumbersToWords numbersToWords = new NumbersToWords("0.120");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void FractionalButNoDollarsMultipleZeroes()
    {
        NumbersToWords numbersToWords = new NumbersToWords("0.1200");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TWELVE CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test for a valid number with cents but no dollars
    [Test]
    public void ValidNumberButWithDecimal()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1234.");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE THOUSAND TWO HUNDRED AND THIRTY-FOUR DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }


    // Magnitude tests
    [Test]
    public void TestMagnitude()
    {
        NumbersToWords numbersToWords = new NumbersToWords("100");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE HUNDRED DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestMagnitude2()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE THOUSAND DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestMagnitude3()
    {
        NumbersToWords numbersToWords = new NumbersToWords("10000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "TEN THOUSAND DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestMagnitude4()
    {
        NumbersToWords numbersToWords = new NumbersToWords("100000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE HUNDRED THOUSAND DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestMagnitude5()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE MILLION DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    //trailing zeroes
    [Test]
    public void TestTrailingZeroes()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000000.000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE MILLION DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestTrailingZeroes2()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000000.0000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE MILLION DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestTrailingZeroes3()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000000.00000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE MILLION DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestTrailingZeroes4()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000000.000010");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE MILLION DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestTrailingZeroes5()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000000.100000");
        string? words = numbersToWords.NumberToWordsFunc();
        string expected = "ONE MILLION DOLLARS AND TEN CENTS";
        Assert.That(words, Is.EqualTo(expected));
    }

    [Test]
    public void TestOnlyDecimalPoint()
    {
        NumbersToWords numbersToWords = new NumbersToWords(".");
        string? words = numbersToWords.NumberToWordsFunc();
        // expects an empty string
        string expected = "";
        Assert.That(words, Is.EqualTo(expected));
    }

    // Test if there is a comma rather than a decimal point
    [Test]
    public void TestIfCommas()
    {
        NumbersToWords numbersToWords = new NumbersToWords("1000000,00");
        string? words = numbersToWords.NumberToWordsFunc(); // should still split the number
        string expected = "ONE MILLION DOLLARS";
        Assert.That(words, Is.EqualTo(expected));
    }
}