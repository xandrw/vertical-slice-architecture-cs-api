using Application.Features.Admin.Users.Common.Http;
using Application.Features.Admin.Users.Common.Http.Swagger;
using Application.Features.Admin.Users.CreateUser.Http.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.CreateUser.Http;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = Role.Admin)]
public class CreateUserEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "createUser")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Create User", Tags = ["Admin / Users"])]
    [SwaggerRequestExample(typeof(CreateUserRequest), typeof(CreateUserRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(UserResponseExample))]
    [SwaggerResponse(
        statusCode: StatusCodes.Status201Created,
        Description = "User Created",
        Type = typeof(UserResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, Description = "Bad Request")]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, Description = "Forbidden")]
    [SwaggerResponse(statusCode: StatusCodes.Status409Conflict, Description = "Conflict")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var user = await mediator.Send(request);
        var response = new UserResponse
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
}