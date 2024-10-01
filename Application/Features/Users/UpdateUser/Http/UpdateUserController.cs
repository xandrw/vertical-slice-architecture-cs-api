using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Users.UpdateUser.Http;

[ApiController]
[Route("users/{id}")]
[Authorize(Roles = Role.Admin)]
public class UpdateUserController(IMediator mediator) : ControllerBase
{
    
}