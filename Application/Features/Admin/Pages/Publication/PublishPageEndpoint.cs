using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Features.Admin.Pages.Publication;

[ApiController]
[Route("api/admin/pages/{id}/publish")]
[Produces("application/json")]
[Authorize(Roles = Role.Admin)]
public class PublishPageEndpoint(PagePublicationManager pagePublicationManager) : ControllerBase
{
    [HttpPatch(Name = "publishPage")]
    [SwaggerOperation(Summary = "Publish Page", Tags = ["Admin / Pages"])]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id)
    {
        await pagePublicationManager.PublishPageById(id);

        return new NoContentResult();
    }
}