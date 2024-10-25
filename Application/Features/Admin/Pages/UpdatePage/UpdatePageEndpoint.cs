using Application.Common.Http.Responses;
using Application.Features.Admin.Pages.Common;
using Application.Features.Admin.Pages.Common.Swagger;
using Application.Features.Admin.Pages.UpdatePage.Command;
using Application.Features.Admin.Pages.UpdatePage.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.UpdatePage;

[ApiController]
[Route("api/admin/pages/{id}")]
[Produces("application/json")]
[Authorize(Roles = Role.Admin)]
public class UpdatePageEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPut(Name = "updatePage")]
    [SwaggerOperation(Summary = "Update Page", Tags = ["Admin / Pages"])]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "Page Updated", Type = typeof(PageResponse))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PageResponseExample))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Type = typeof(UnprocessableEntityResponse))]
    [SwaggerRequestExample(typeof(UpdatePageRequest), typeof(UpdatePageRequestExample))]
    public async Task<ActionResult> Put([FromBody] UpdatePageRequest request, int id)
    {
        var page = await mediator.Send(
            new UpdatePageCommand(id, request.Name, request.Title, request.Description, request.Sections)
        );

        return new OkObjectResult(PageResponse.CreateFrom(page));
    }
}