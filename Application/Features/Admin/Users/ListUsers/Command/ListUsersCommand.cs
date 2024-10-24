using Domain.Users;
using MediatR;

namespace Application.Features.Admin.Users.ListUsers.Command;

public class ListUsersCommand(int pageNumber = 1, int pageSize = 10) : IRequest<(int total, IList<User> users)>
{
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}