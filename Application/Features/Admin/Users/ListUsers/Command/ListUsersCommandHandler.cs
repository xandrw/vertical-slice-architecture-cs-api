using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Users.ListUsers.Command;

public class ListUsersCommandHandler(IDataProxy<User> dataProxy)
    : IRequestHandler<ListUsersCommand, (int total, IList<User> users)>
{
    public async Task<(int, IList<User>)> Handle(
        ListUsersCommand request,
        CancellationToken cancellationToken
    )
    {
        var total = await dataProxy.Query().CountAsync(cancellationToken);
        var users = await dataProxy.Query()
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return (total, users);
    }
}