using Application.Features.Auth.Login.Http.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.Login.Http;

[ApiController]
[Route("login")]
[AllowAnonymous]
public class LoginController(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "login")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "User Login")]
    [SwaggerRequestExample(typeof(LoginRequest), typeof(LoginRequestExample))]
    [SwaggerResponse(statusCode: 200, Description = "Logged in", Type = typeof(LoginResponse))]
    [SwaggerResponse(statusCode: 401, Description = "Unauthorized")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var jwtToken = await mediator.Send(MakeCommand(request.Email, request.Password));
        var response = new LoginResponse { Token = jwtToken };

        return Ok(response);
    }

    private LoginCommand MakeCommand(string email, string password)
    {
        return new LoginCommand(email, password);
    }
}