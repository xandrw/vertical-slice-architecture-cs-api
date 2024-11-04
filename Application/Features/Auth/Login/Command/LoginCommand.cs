using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Login.Command;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;

public class LoginCommandHandler(
    IRepository<User> usersRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher
) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await usersRepository.Query().SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null || !user.VerifyPassword(command.Password, passwordHasher.VerifyPassword))
        {
            throw new UnauthorizedHttpException();
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            User = new LoginResponse.LoginUserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            }
        };
    }
}