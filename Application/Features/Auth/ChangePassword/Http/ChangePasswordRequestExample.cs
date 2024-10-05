using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Auth.ChangePassword.Http;

public class ChangePasswordRequestExample : IExamplesProvider<ChangePasswordRequest>
{
    public ChangePasswordRequest GetExamples()
    {
        return new ChangePasswordRequest { Password = "password" };
    }
}