using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.Login.Swagger;

public class LoginRequestExample : IExamplesProvider<LoginRequest>
{
    public LoginRequest GetExamples()
    {
        return new LoginRequest("example@email.com", "password");
    }
}