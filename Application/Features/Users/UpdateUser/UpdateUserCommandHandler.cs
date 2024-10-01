using Domain;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.UpdateUser;

public class UpdateUserCommandHandler(IRepository<User> repository)
    : IRequestHandler<UpdateUserCommand, User>
{
    public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.Query()
            .SingleOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (command.Email is not null)
        {
            user.ChangeEmail(command.Email);
        }
        
        if (command.Password is not null)
        {
            user.ChangePassword(command.Password);
        }

        if (command.Role is not null)
        {
            user.ChangeRole(command.Role);
        }
        
        await repository.SaveChangesAsync(cancellationToken);

        return user;
    }
}