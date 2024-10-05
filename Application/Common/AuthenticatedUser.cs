using System.Security.Claims;
using Application.Common.Http.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Common;

public class AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
{
    private readonly ClaimsPrincipal _user =
        httpContextAccessor.HttpContext?.User ?? throw new UnauthorizedHttpException();

    public int Id => int.Parse(
        _user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedHttpException());
    
    public string Email => _user.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedHttpException();
    
    public string Role => _user.FindFirstValue(ClaimTypes.Role) ?? throw new UnauthorizedHttpException();
}