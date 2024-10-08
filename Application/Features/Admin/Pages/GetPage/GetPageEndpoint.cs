using Application.Common.Http.Exceptions;
using Application.Features.Admin.Pages.Common.Http;
using Application.Features.Admin.Pages.Common.Http.Swagger;
using Application.Interfaces;
using Domain.Pages;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.GetPage;

[ApiController]
[Route("api/admin/pages/{id}")]
[Authorize(Roles = Role.Admin)]
public class GetPageEndpoint(IDataProxy<Page> dataProxy) : ControllerBase
{
    [HttpGet(Name = "getPage")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get Page", Tags = ["Admin / Pages"])]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PageResponseExample))]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, Description = "Page retrieved", Type = typeof(PageResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status401Unauthorized, Description = "Unauthorized")]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, Description = "Forbidden")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, Description = "Not Found")]
    public async Task<IActionResult> Get(int id)
    {
        var page = await dataProxy.Query().Include(p => p.Sections).FirstOrDefaultAsync(u => u.Id == id);

        if (page is null) throw new NotFoundHttpException<Page>();

        return Ok(PageResponse.CreateFromEntity(page));
    }
}