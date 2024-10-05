using Application.Common;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.ChangePassword.Http;

[ApiController]
[Route("api/change-password")]
[Authorize(Roles = $"{Role.Admin},{Role.Author}")]
public class ChangePasswordEndpoint(IMediator mediator, AuthenticatedUser authenticatedUser) : ControllerBase
{
    [HttpPost(Name = "changePassword")]
    [SwaggerOperation(Summary = "Change Password", Tags = ["Auth"])]
    [SwaggerRequestExample(typeof(ChangePasswordRequest), typeof(ChangePasswordRequestExample))]
    [SwaggerResponse(statusCode: StatusCodes.Status204NoContent, Description = "Password Changed")]
    [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, Description = "Bad Request")]
    [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, Description = "Forbidden")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, Description = "Not Found")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var command = MakeCommand(authenticatedUser.Id, request.Password);
        await mediator.Send(command);

        return NoContent();
    }

    private ChangePasswordCommand MakeCommand(int id, string password)
    {
        return new ChangePasswordCommand(id, password);
    }
}