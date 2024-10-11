using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Config;

public static class AuthConfig
{
    public static void ConfigureAuth(this IServiceCollection services, IConfiguration config)
    {
        var key = Encoding.ASCII.GetBytes(
            config["Jwt:Key"] ??
            throw new InvalidOperationException("appsettings.json Jwt:Key is missing"));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RoleClaimType = ClaimTypes.Role,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        services.AddAuthorization();
    }
}