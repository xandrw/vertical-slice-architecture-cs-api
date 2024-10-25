using Application.Features.Admin.Pages.DeletePage.Command;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Admin.Pages.DeletePage;

[ApiController]
[Route("api/admin/pages/{id}")]
[Authorize(Roles = Role.Admin)]
public class DeletePageEndpoint(IMediator mediator) : ControllerBase
{
    [HttpDelete(Name = "deletePage")]
    [SwaggerOperation(Summary = "Delete Page", Tags = ["Admin / Pages"])]
    [SwaggerResponse(StatusCodes.Status204NoContent, Description = "Page Deleted")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<NoContentResult> Delete(int id)
    {
        await mediator.Send(new DeletePageCommand(id));
        
        return new NoContentResult();
    }
}