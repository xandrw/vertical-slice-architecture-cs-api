using Application.Common.Http.Responses;
using Application.Common.Http.Swagger;
using Application.Features.Admin.Pages.Common.Http;
using Application.Features.Admin.Pages.Common.Http.Swagger;
using Application.Features.Admin.Pages.CreatePage.Command;
using Application.Features.Admin.Pages.CreatePage.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.CreatePage;

[ApiController]
[Route("api/admin/pages")]
[Produces("application/json")]
[Authorize(Roles = Role.Admin)]
public class CreatePageEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "createPage")]
    [SwaggerOperation(Summary = "Create Page", Tags = ["Admin / Pages"])]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PageResponse))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(PageResponseExample))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Type = typeof(UnprocessableEntityResponse))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(UnprocessableResponseExample))]
    [SwaggerRequestExample(typeof(CreatePageRequest), typeof(CreatePageRequestExample))]
    public async Task<IActionResult> Post([FromBody] CreatePageRequest request)
    {
        var page = await mediator.Send(request);
        var response = PageResponse.CreateFromEntity(page);

        return new CreatedResult { StatusCode = 201, Value = response, ContentTypes = { "application/json" } };
    }
}