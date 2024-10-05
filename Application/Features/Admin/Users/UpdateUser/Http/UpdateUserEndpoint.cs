using Application.Features.Admin.Users.Common.Http;
using Application.Features.Admin.Users.Common.Http.Swagger;
using Application.Features.Admin.Users.UpdateUser.Http.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.UpdateUser.Http;

[ApiController]
[Route("api/admin/users/{id}")]
[Authorize(Roles = Role.Admin)]
public class UpdateUserEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPut(Name = "updateUser")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Update User", Tags = ["Admin / Users"])]
    [SwaggerRequestExample(typeof(UpdateUserRequest), typeof(UpdateUserRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK, Description = "User Updated", Type = typeof(UserResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, Description = "Bad Request")]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, Description = "Forbidden")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, Description = "Not Found")]
    [SwaggerResponse(statusCode: StatusCodes.Status409Conflict, Description = "Conflict")]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateUserRequest request)
    {
        var command = MakeCommand(id, request.Email, request.Role);
        var user = await mediator.Send(command);

        return Ok(new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        });
    }

    private UpdateUserCommand MakeCommand(int id, string email, string role)
    {
        return new UpdateUserCommand(id, email, role);
    }
}