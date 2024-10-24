using Application.Features.Auth.Login.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.Login;

[ApiController]
[AllowAnonymous]
[Route("api/login")]
[Produces("application/json")]
public class LoginEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "login")]
    [SwaggerOperation(Summary = "Login", Tags = ["Auth"])]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "Logged in", Type = typeof(LoginResponse))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginResponseExample))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginRequestExample))]
    public async Task<IActionResult> Post([FromBody] LoginRequest request)
    {
        var response = await mediator.Send(request);

        return new OkObjectResult(response);
    }
}