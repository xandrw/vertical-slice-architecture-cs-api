using Domain.Users;
using MediatR;

namespace Application.Features.Users.CreateUser;

public class CreateUserCommand(string email, string password, string role) : IRequest<User>
{
    public string Email { get; } = email;
    public string Password { get; } = password;
    public string Role { get; } = role;
}