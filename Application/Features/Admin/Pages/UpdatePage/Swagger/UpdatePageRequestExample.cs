using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.UpdatePage.Swagger;

public class UpdatePageRequestExample : IExamplesProvider<UpdatePageRequest>
{
    public UpdatePageRequest GetExamples()
    {
        return new UpdatePageRequest("Page Name", "Page Title", "Page Description")
        {
            Sections = [
                new("Section Category", "Section to be Updated Name", "Section Value") { Id = 1 },
                new("Section Category", "2nd Section to be Created Name", "2nd Section Value")
            ]
        };
    }
}