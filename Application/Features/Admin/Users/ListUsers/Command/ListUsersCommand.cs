using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.ListUsers.Command;

public record ListUsersCommand(int PageNumber = 1, int PageSize = 10) : IRequest<(int total, IList<User> users)>;

public class ListUsersCommandHandler(IRepository<User> usersRepository)
    : IRequestHandler<ListUsersCommand, (int total, IList<User> users)>
{
    public async Task<(int, IList<User>)> Handle(ListUsersCommand command, CancellationToken cancellationToken)
    {
        var total = await usersRepository.Query().CountAsync(cancellationToken);
        var users = await usersRepository.Query()
            .Skip((command.PageNumber - 1) * command.PageSize)
            .Take(command.PageSize)
            .ToListAsync(cancellationToken);

        return (total, users);
    }
}