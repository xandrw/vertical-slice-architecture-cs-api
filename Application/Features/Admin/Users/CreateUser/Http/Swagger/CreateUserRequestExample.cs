using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.CreateUser.Http.Swagger;

public class CreateUserRequestExample : IExamplesProvider<CreateUserRequest>
{
    public CreateUserRequest GetExamples()
    {
        return new CreateUserRequest("email@example.com", "Author|Admin", "password");
    }
}