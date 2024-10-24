using Application.Common.Http.Responses;
using Application.Common.Http.Swagger;
using Application.Features.Admin.Users.Common.Http;
using Application.Features.Admin.Users.Common.Http.Swagger;
using Application.Features.Admin.Users.CreateUser.Command;
using Application.Features.Admin.Users.CreateUser.Swagger;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.CreateUser;

[ApiController]
[Route("api/admin/users")]
[Produces("application/json")]
[Authorize(Roles = Role.Admin)]
public class CreateUserEndpoint(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "createUser")]
    [SwaggerOperation(Summary = "Create User", Tags = ["Admin / Users"])]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(UserResponse))]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(UserResponseExample))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, Type = typeof(UnprocessableEntityResponse))]
    [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(UnprocessableResponseExample))]
    [SwaggerRequestExample(typeof(CreateUserRequest), typeof(CreateUserRequestExample))]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
    {
        var user = await mediator.Send(request);

        return new CreatedResult { Value = UserResponse.CreateFrom(user), ContentTypes = { "application/json" } };
    }
}