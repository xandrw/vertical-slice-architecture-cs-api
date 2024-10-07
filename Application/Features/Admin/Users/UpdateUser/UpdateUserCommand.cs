using Domain.Users;
using MediatR;

namespace Application.Features.Admin.Users.UpdateUser;

public class UpdateUserCommand(int id, string email, string role) : IRequest<User>
{
    public int Id { get; } = id;
    public string Email { get; } = email;
    public string Role { get; } = role;
}