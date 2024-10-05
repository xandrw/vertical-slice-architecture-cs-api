using Application.Interfaces.External.PostmanEcho;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.GetCurrentDateTime;

[ApiController]
[Route("api/current-date-time")]
[Authorize(Roles = $"{Role.Admin},{Role.Author}")]
public class GetCurrentDateTimeEndpoint(IPostmanEchoTimeClient postmanEchoTimeClient) : ControllerBase
{
    [HttpGet(Name = "getCurrentDateTime")]
    [Produces("text/plain")]
    [SwaggerOperation(Summary = "Get Current Date and Time", Tags = ["Custom"])]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(string))]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK,
        Description = "Current Date and Time Retrieved: Sat, 05 Oct 2024 22:47:16 GMT",
        Type = typeof(string))]
    [SwaggerResponse(
        statusCode: StatusCodes.Status204NoContent,
        Description = "External Service could not be reached or failed to respond")]
    [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
    public async Task<IActionResult> GetCurrentDateTime()
    {
        var currentDateTime = await postmanEchoTimeClient.Now();

        if (currentDateTime is null)
        {
            return NoContent();
        }
        
        return Ok(currentDateTime);
    }
}