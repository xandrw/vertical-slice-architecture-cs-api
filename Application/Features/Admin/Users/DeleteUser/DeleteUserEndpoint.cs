using Application.Features.Admin.Users.DeleteUser.Command;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Admin.Users.DeleteUser;

[ApiController]
[Route("api/admin/users/{id}")]
[Authorize(Roles = Role.Admin)]
public class DeleteUserEndpoint(IMediator mediator) : ControllerBase
{
    [HttpDelete(Name = "deleteUser")]
    [SwaggerOperation(Summary = "Delete User", Tags = ["Admin / Users"])]
    [SwaggerResponse(StatusCodes.Status204NoContent, Description = "User Deleted")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<NoContentResult> Delete(int id)
    {
        await mediator.Send(new DeleteUserCommand(id));

        return new NoContentResult();
    }
}