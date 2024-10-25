using Application.Common.Http.Responses;
using Application.Features.Admin.Users.Common;
using Application.Features.Admin.Users.Common.Swagger;
using Application.Features.Admin.Users.UpdateUser.Command;
using Application.Features.Admin.Users.UpdateUser.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.UpdateUser;

[ApiController]
[Route("api/admin/users/{id}")]
[Produces("application/json")]
[Authorize(Roles = Role.Admin)]
public class UpdateUserEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPut(Name = "updateUser")]
    [SwaggerOperation(Summary = "Update User", Tags = ["Admin / Users"])]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "User Updated", Type = typeof(UserResponse))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Type = typeof(UnprocessableEntityResponse))]
    [SwaggerRequestExample(typeof(UpdateUserRequest), typeof(UpdateUserRequestExample))]
    public async Task<ActionResult> Put([FromBody] UpdateUserRequest request, int id)
    {
        var user = await mediator.Send(new UpdateUserCommand(id, request.Email, request.Role));

        return new OkObjectResult(UserResponse.CreateFrom(user));
    }
}