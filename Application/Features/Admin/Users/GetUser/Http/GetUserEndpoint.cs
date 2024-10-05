using Application.Features.Admin.Users.Common.Http;
using Application.Features.Admin.Users.Common.Http.Exceptions;
using Application.Features.Admin.Users.Common.Http.Swagger;
using Application.Interfaces;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.GetUser.Http;

[ApiController]
[Route("api/admin/users/{id}")]
[Authorize(Roles = Role.Admin)]
public class GetUserEndpoint(IDataProxy<User> dataProxy) : ControllerBase
{
    [HttpGet(Name = "getUser")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get User", Tags = ["Admin / Users"])]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, Description = "User retrieved", Type = typeof(UserResponse))]
    [SwaggerResponse(statusCode: StatusCodes.Status403Forbidden, Description = "Forbidden")]
    [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, Description = "Not Found")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await dataProxy.Query().FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        return Ok(new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        });
    }
}