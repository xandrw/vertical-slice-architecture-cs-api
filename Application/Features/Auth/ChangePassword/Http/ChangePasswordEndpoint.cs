using Application.Common;
using Application.Common.Http.Responses;
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
    [SwaggerResponse(StatusCodes.Status204NoContent, Description = "Password Changed")]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Type = typeof(UnprocessableEntityResponse))]
    [SwaggerRequestExample(typeof(ChangePasswordRequest), typeof(ChangePasswordRequestExample))]
    public async Task<ActionResult> Post([FromBody] ChangePasswordRequest request)
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