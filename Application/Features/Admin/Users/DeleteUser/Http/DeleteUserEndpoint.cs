using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Admin.Users.DeleteUser.Http;

[ApiController]
[Route("api/admin/users/{id}")]
[Authorize(Roles = Role.Admin)]
public class DeleteUserEndpoint(IMediator mediator) : ControllerBase
{
    [HttpDelete(Name = "deleteUser")]
    [SwaggerOperation(Summary = "Delete User", Tags = ["Admin / Users"])]
    [SwaggerResponse(statusCode: StatusCodes.Status204NoContent, Description = "User Deleted, no content")]
    [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, Description = "Forbidden")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, Description = "Not Found")]
    public async Task<NoContentResult> Delete(int id)
    {
        await mediator.Send(new DeleteUserCommand(id));

        return NoContent();
    }
}