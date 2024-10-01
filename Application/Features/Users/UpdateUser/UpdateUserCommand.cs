using Domain.Users;
using MediatR;

namespace Application.Features.Users.UpdateUser;

public class UpdateUserCommand(int id, string? email, string? password, string? role) : IRequest<User>
{
    public int Id { get; } = id;
    public string? Email { get; } = email;
    public string? Password { get; } = password;
    public string? Role { get; } = role;
}