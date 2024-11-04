using Application.Common.Http.Responses;
using Application.Features.Admin.Users.Common;
using Application.Features.Admin.Users.ListUsers.Command;
using Application.Features.Admin.Users.ListUsers.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.ListUsers;

[ApiController]
[Route("api/admin/users")]
[Produces("application/json")]
[Authorize(Roles = Role.Admin)]
public class ListUsersEndpoint(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "listUsers")]
    [SwaggerOperation(Summary = "List Paginated Users", Tags = ["Admin / Users"])]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PaginatedListResponse<UserResponse>))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ListUsersResponseExample))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
    {
        var (total, users) = await mediator.Send(new ListUsersCommand(pageNumber, pageSize));

        var response = new PaginatedListResponse<UserResponse>
        {
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        foreach (User user in users)
        {
            response.Items.Add(UserResponse.CreateFrom(user));
        }

        return new OkObjectResult(response);
    }
}