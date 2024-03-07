namespace API.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[EnableCors("_myAllowSpecificOrigins")]
public class NumbersToWordsController : ControllerBase
{
    /// <summary>
    /// This method will convert the number to words
    /// </summary>
    /// <param name="number">he numeric string to convert.</param>
    /// <returns>The english string representation of the number</returns>
    [HttpGet("{number}")]
    [ProducesResponseType(200, Type = typeof(string))]
    [ProducesResponseType(400, Type = typeof(string))]
    [ProducesResponseType(404, Type = typeof(string))]
    [ProducesResponseType(500, Type = typeof(string))]
    public ActionResult<string> NumberToWords(string number)
    {
        // add proper error handling for 200 and 400 status codes
        NumbersToWords numbersToWords = new NumbersToWords();
        string? words = numbersToWords.NumberToWordsFunc(number);
        var headers = Response.Headers;
        headers.Add("Access-Control-Allow-Origin", "*");

        // 404 - Not Found
        if (string.IsNullOrEmpty(words))
        {
            return NotFound("NOT FOUND: Please provide a valid number or number was out of range.");
        }

        // 400 - Bad Request
        if (string.IsNullOrWhiteSpace(number))
        {
            return BadRequest("BAD REQUEST: Please provide a number or number was out of range");
        }
        
        // 500
        if (string.IsNullOrEmpty(words))
        {
            return StatusCode(500, "Internal Server Error");
        }
        

        // 200
        return Ok(new { words });
    }
}