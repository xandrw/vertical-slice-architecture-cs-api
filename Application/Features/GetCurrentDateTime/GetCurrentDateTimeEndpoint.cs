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
[Produces("text/plain")]
[Authorize(Roles = $"{Role.Admin},{Role.Author}")]
public class GetCurrentDateTimeEndpoint(IPostmanEchoTimeClient postmanEchoTimeClient) : ControllerBase
{
    [HttpGet(Name = "getCurrentDateTime")]
    [SwaggerOperation(Summary = "Get Current Date and Time", Tags = ["Custom"])]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK,
        Description = "Current Date and Time Retrieved: Sat, 05 Oct 2024 22:47:16 GMT",
        Type = typeof(string)
    )]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(string))]
    [SwaggerResponse(
        statusCode: StatusCodes.Status204NoContent,
        Description = "External Service could not be reached or failed to respond"
    )]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get()
    {
        var currentDateTime = await postmanEchoTimeClient.Now();

        if (currentDateTime is null)
        {
            return new NoContentResult();
        }
        
        return new OkObjectResult(currentDateTime);
    }
}