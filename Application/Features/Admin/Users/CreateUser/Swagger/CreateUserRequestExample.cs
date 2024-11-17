using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.CreateUser.Swagger;

public class CreateUserRequestExample : IExamplesProvider<CreateUserRequest>
{
    public CreateUserRequest GetExamples()
    {
        return new CreateUserRequest("example@email.com", "Author|Admin", "password");
    }
}