using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Users.CreateUser.Http.Swagger;

public class CreateUserRequestExample : IExamplesProvider<CreateUserRequest>
{
    public CreateUserRequest GetExamples()
    {
        return new CreateUserRequest
        {
            Email = "email@example.com",
            Password = "examplePassword",
            Role = "Author|Admin"
        };
    }
}