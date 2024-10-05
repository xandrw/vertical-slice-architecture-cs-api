using Domain.Users;

namespace Application.Features.Auth;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}