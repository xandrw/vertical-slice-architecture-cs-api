using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.Common.Swagger;

class UserResponseExample : IExamplesProvider<UserResponse>
{
    public UserResponse GetExamples()
    {
        return new UserResponse
        {
            Id = 1,
            Email = "example@email.com",
            Role = "Author|Admin"
        };
    }
}