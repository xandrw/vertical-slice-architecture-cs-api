using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.ListUsers.Command;

public class ListUsersCommand(int pageNumber = 1, int pageSize = 10) : IRequest<(int total, IList<User> users)>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}

public class ListUsersCommandHandler(IDataProxy<User> usersProxy)
    : IRequestHandler<ListUsersCommand, (int total, IList<User> users)>
{
    public async Task<(int, IList<User>)> Handle(ListUsersCommand command, CancellationToken cancellationToken)
    {
        var total = await usersProxy.Query().CountAsync(cancellationToken);
        var users = await usersProxy.Query()
            .Skip((command.PageNumber - 1) * command.PageSize)
            .Take(command.PageSize)
            .ToListAsync(cancellationToken);

        return (total, users);
    }
}