using Application.Features.Auth.Login.Http.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.Login.Http;

[ApiController]
[Route("api/login")]
[AllowAnonymous]
public class LoginEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "login")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Login", Tags = ["Auth"])]
    [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, Description = "Logged in", Type = typeof(LoginResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
    public async Task<IActionResult> Post([FromBody] LoginRequest request)
    {
        var response = await mediator.Send(request);

        return Ok(response);
    }
}