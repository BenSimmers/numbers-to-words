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
        NumbersToWords numbersToWords = new NumbersToWords(number);
        string? words = numbersToWords.NumberToWordsFunc();
        var headers = Response.Headers;
        headers.Add("Access-Control-Allow-Origin", "*");
        
        // 400 - Bad Request
        if (string.IsNullOrWhiteSpace(number))
        {
            return BadRequest("BAD REQUEST: Please provide a number or number was out of range");
        }
        
        // 500 - Internal Server Error
        if (string.IsNullOrEmpty(words))
        {
            return StatusCode(500, "Internal Server Error");
        }
        

        // 200 - OK
        return Ok(new { words });
    }
}