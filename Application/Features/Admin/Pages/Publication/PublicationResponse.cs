namespace Application.Features.Admin.Pages.Publication;

public class PublicationResponse(DateTime? publishedAt)
{
    public DateTime? PublishedAt { get; private set; } = publishedAt;
}