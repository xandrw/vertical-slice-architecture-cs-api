using Swashbuckle.AspNetCore.Filters;

namespace Application.Features.Admin.Pages.Publication.Swagger;

public class PublicationResponseExample : IExamplesProvider<PublicationResponse>
{
    public PublicationResponse GetExamples()
    {
        return new PublicationResponse(new DateTime(2024, 01, 01));
    }
}