using System.Security.Cryptography;
using System.Text;
using Application.Features.Users.CreateUser.Http;
using Domain;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.CreateUser;

public class CreateUserCommandHandler(IRepository<User> repository) : IRequestHandler<CreateUserCommand, User>
{
    public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existingUser = await repository.Query()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (existingUser is not null)
        {
            throw new UserExistsException();
        }

        using var hmac = new HMACSHA512();
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.Password));
        var passwordSalt = hmac.Key;

        var user = new User(command.Email, passwordHash, passwordSalt, command.Role);


        repository.Add(user);
        await repository.SaveChangesAsync(cancellationToken);

        return user;
    }
}