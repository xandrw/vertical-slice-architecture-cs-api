using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Login.Command;

public class LoginCommandHandler(
    IDataProxy<User> dataProxy,
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher
) : IRequestHandler<LoginRequest, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await dataProxy.Query().SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null || !user.VerifyPassword(request.Password, passwordHasher.VerifyPassword))
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