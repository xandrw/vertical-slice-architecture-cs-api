using Application.Common.Http.Responses;
using Application.Features.Admin.Pages.Common;
using Application.Features.Admin.Pages.ListPages.Command;
using Application.Features.Admin.Pages.ListPages.Swagger;
using Domain.Pages;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.ListPages;

[ApiController]
[Route("api/admin/pages")]
[Produces("application/json")]
[Authorize(Roles = $"{Role.Admin},{Role.Author}")]
public class ListPagesEndpoint(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "listPages")]
    [SwaggerOperation(Summary = "List Paginated Pages", Tags = ["Admin / Pages"])]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PaginatedListResponse<PageResponse>))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ListPagesResponseExample))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
    {
        var (total, pages) = await mediator.Send(new ListPagesCommand(pageNumber, pageSize));

        var response = new PaginatedListResponse<PageResponse>
        {
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        foreach (Page page in pages)
        {
            response.Items.Add(PageResponse.CreateFrom(page));
        }

        return new OkObjectResult(response);
    }
}