using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.UpdatePage.Swagger;

public class UpdatePageRequestExample : IExamplesProvider<UpdatePageRequest>
{
    public UpdatePageRequest GetExamples()
    {
        return new UpdatePageRequest("Page Name", "Page Title", "Page Description")
        {
            Sections = [
                new UpdatePageRequestSectionItem("Section Category", "Section Name", "Section Value") { Id = 1 },
                new UpdatePageRequestSectionItem("Section Category", "2nd Section Name", "2nd Section Value")
            ]
        };
    }
}