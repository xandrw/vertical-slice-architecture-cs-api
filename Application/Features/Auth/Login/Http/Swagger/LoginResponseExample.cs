using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.Login.Http.Swagger;

public class LoginResponseExample : IExamplesProvider<LoginResponse>
{
    public LoginResponse GetExamples()
    {
        return new LoginResponse
        {
            Token = "Bearer auth.jwt.token"
        };
    }
}