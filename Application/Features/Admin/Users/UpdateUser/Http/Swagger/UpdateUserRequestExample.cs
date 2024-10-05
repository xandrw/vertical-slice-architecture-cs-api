using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.UpdateUser.Http.Swagger;

public class UpdateUserRequestExample : IExamplesProvider<UpdateUserRequest>
{
    public UpdateUserRequest GetExamples()
    {
        return new UpdateUserRequest()
        {
            Email = "email@example.com",
            Role = "Author|Admin"
        };
    }
}