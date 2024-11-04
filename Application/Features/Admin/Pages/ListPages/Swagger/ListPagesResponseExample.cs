using Application.Common.Http.Responses;
using Application.Features.Admin.Pages.Common;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.ListPages.Swagger;

public class ListPagesResponseExample : IExamplesProvider<PaginatedListResponse<PageResponse>>
{
    public PaginatedListResponse<PageResponse> GetExamples()
    {
        return new PaginatedListResponse<PageResponse>
        {
            Items = new List<PageResponse>
            {
                new()
                {
                    Id = 1,
                    Name = "Page Name",
                    Title = "Page Title",
                    Slug = "page-title",
                    Description = "Page Description",
                    PublishedAt = null,
                    Sections =
                    [
                        new()
                        {
                            Id = 1,
                            Category = "Section Category",
                            Name = "Section Name",
                            Value = "Section Value"
                        }
                    ]
                }
            },
            TotalCount = 1,
            PageNumber = 1,
            PageSize = 10
        };
    }
}