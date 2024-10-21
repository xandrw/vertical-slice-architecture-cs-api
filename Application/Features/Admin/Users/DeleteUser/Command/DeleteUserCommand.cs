using MediatR;

namespace Application.Features.Admin.Users.DeleteUser.Command;

public class DeleteUserCommand(int id) : IRequest
{
    public int Id { get; } = id;
}