using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.Common.Swagger;

public class PageResponseExample : IExamplesProvider<PageResponse>
{
    public PageResponse GetExamples()
    {
        return new PageResponse
        {
            Id = 1,
            Name = "Page Name",
            Title = "Page Title",
            Description = "Page Description",
            Slug = "page-title",
            Sections =
            [
                new SectionResponse
                {
                    Id = 1, Category = "Section Category Name", Name = "Section Name", Value = "Section Name"
                }
            ]
        };
    }
}