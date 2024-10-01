using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Features.Auth.Login.Http;
using Domain;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Auth.Login;

public class LoginCommandHandler(IRepository<User> repository, IConfiguration config)
    : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.Query().SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null || !user.VerifyPassword(command.Password)) throw new LoginFailedException();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(
            config["Jwt:Key"] ?? throw new InvalidOperationException("appsettings.json Jwt:Key is missing"));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}