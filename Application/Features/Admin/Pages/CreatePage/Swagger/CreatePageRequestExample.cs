using Application.Features.Admin.Pages.CreatePage.Command;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.CreatePage.Swagger;

public class CreatePageRequestExample : IExamplesProvider<CreatePageRequest>
{
    public CreatePageRequest GetExamples()
    {
        return new CreatePageRequest("Page", "Page Title", "Page Description")
        {
            Sections = new List<CreatePageRequestSectionItem>
            {
                new("Category", "Name", "value")
            }
        };
    }
}