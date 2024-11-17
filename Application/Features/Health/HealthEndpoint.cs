using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Health;

[ApiController]
[Route("api/health")]
[Produces("text/plain")]
[AllowAnonymous]
public class HealthEndpoint : ControllerBase
{
    [HttpGet(Name = "health")]
    [SwaggerOperation(Summary = "Health Check", Tags = ["Health"])]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    public Task<IActionResult> Get()
    {
        return Task.FromResult<IActionResult>(new NoContentResult());
    }
}