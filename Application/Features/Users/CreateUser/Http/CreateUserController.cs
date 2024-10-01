using Application.Features.Users.CreateUser.Http.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Users.CreateUser.Http;

[ApiController]
[Route("users")]
[Authorize(Roles = Role.Admin)]
public class CreateUserController(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "createUser")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Create User")]
    [SwaggerRequestExample(typeof(CreateUserRequest), typeof(CreateUserRequestExample))]
    [SwaggerResponse(statusCode: 201, Description = "User Created", Type = typeof(CreateUserResponse))]
    [SwaggerResponse(statusCode: 403, Description = "Forbidden")]
    [SwaggerResponse(statusCode: 400, Description = "Bad Request")]
    [SwaggerResponse(statusCode: 409, Description = "Conflict")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var createUserCommand = MakeCreateUserCommand(request.Email, request.Password, request.Role);
        var user = await mediator.Send(createUserCommand);
        var response = new CreateUserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        };

        return new CreatedResult
        {
            StatusCode = 201,
            Value = response,
            ContentTypes = { "application/json" }
        };
    }

    private CreateUserCommand MakeCreateUserCommand(string email, string password, string role)
    {
        return new CreateUserCommand(email, password, role);
    }
}