using Application.Common.Http.Exceptions;
using Application.Features.Admin.Users.Common.Http;
using Application.Features.Admin.Users.Common.Http.Swagger;
using Application.Interfaces;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.GetUser;

[ApiController]
[Route("api/admin/users/{id}")]
[Produces("application/json")]
[Authorize(Roles = Role.Admin)]
public class GetUserEndpoint(IDataProxy<User> dataProxy) : ControllerBase
{
    [HttpGet(Name = "getUser")]
    [SwaggerOperation(Summary = "Get User", Tags = ["Admin / Users"])]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(UserResponse))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserResponseExample))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var user = await dataProxy.Query().FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) throw new NotFoundHttpException<User>();

        return Ok(new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        });
    }
}