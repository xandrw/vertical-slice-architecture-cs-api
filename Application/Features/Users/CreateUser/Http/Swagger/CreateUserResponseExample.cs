using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Users.CreateUser.Http.Swagger;

public class CreateUserResponseExample : IExamplesProvider<CreateUserResponse>
{
    public CreateUserResponse GetExamples()
    {
        return new CreateUserResponse
        {
            Id = 1,
            Email = "email@example.com",
            Role = "Author|Admin"
        };
    }
}