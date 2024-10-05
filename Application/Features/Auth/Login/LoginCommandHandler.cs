using Application.Features.Auth.Login.Http;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Auth.Login;

public class LoginCommandHandler(
    IDataProxy<User> dataProxy,
    IJwtTokenGenerator jwtTokenGenerator
) : IRequestHandler<LoginRequest, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await dataProxy.Query().SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null || !user.VerifyPassword(request.Password))
        {
            throw new LoginFailedException();
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