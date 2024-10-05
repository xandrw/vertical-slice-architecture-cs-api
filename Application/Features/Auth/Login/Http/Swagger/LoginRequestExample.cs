using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.Login.Http.Swagger;

public class LoginRequestExample : IExamplesProvider<LoginRequest>
{
    public LoginRequest GetExamples()
    {
        return new LoginRequest("email@example.com", "password");
    }
}