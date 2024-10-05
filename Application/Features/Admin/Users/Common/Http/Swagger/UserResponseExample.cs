using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.Common.Http.Swagger;

class UserResponseExample : IExamplesProvider<UserResponse>
{
    public UserResponse GetExamples()
    {
        return new UserResponse
        {
            Id = 1,
            Email = "email@example.com",
            Role = "Author|Admin"
        };
    }
}