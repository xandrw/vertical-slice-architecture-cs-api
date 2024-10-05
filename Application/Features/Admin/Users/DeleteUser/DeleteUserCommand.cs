using MediatR;

namespace Application.Features.Admin.Users.DeleteUser;

public class DeleteUserCommand(int id) : IRequest
{
    public int Id { get; } = id;
}