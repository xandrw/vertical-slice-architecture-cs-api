using Application.Common.Http.Responses;
using Application.Features.Admin.Users.Common.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Users.ListUsers.Swagger;

public class ListUsersResponseExample : IExamplesProvider<PaginatedListResponse<UserResponse>>
{
    public PaginatedListResponse<UserResponse> GetExamples()
    {
        return new PaginatedListResponse<UserResponse>
        {
            Items = new List<UserResponse>
            {
                new() { Id = 1, Email = "admin@example.com", Role = "Admin"},
                new() { Id = 2, Email = "author@example.com", Role = "Author"}
            },
            TotalCount = 2,
            PageNumber = 1,
            PageSize = 10
        };
    }
}