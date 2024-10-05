using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.Login.Http.Swagger;

public class LoginResponseExample : IExamplesProvider<LoginResponse>
{
    public LoginResponse GetExamples()
    {
        return new LoginResponse
        {
            Token = "auth.jwt.token",
            User = new LoginResponse.LoginUserResponse
            {
                Id = 1,
                Email = "example@email.com",
                Role = "Author|Admin"
            }
        };
    }
}