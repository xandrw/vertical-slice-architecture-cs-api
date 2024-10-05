using Application.Common.Http.Responses;
using Application.Features.Admin.Users.Common.Http;
using Application.Features.Admin.Users.ListUsers.Http.Swagger;
using Application.Interfaces;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.ListUsers.Http;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = Role.Admin)]
public class ListUsersEndpoint(IDataProxy<User> dataProxy) : ControllerBase
{
    [HttpGet(Name = "listUsers")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "List Paginated Users", Tags = ["Admin / Users"])]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ListUsersResponseExample))]
    [SwaggerResponse(
        statusCode: StatusCodes.Status200OK,
        Description = "User list retrieved",
        Type = typeof(PaginatedListResponse<UserResponse>))]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, Description = "Forbidden")]
    //
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
    {
        var total = await dataProxy.Query().CountAsync();
        var users = await dataProxy.Query()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var response = new PaginatedListResponse<UserResponse>
        {
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        foreach (User user in users)
        {
            response.Items.Add(new UserResponse { Id = user.Id, Email = user.Email, Role = user.Role });
        }

        return Ok(response);
    }
}