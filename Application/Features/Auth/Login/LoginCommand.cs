using MediatR;

namespace Application.Features.Auth.Login;

public class LoginCommand(string email, string password) : IRequest<string>
{
    public string Email { get; } = email;
    public string Password { get; } = password;
}