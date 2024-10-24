using System.ComponentModel.DataAnnotations;
using Application.Common.Http.Responses;
using Domain.Pages;

namespace Application.Features.Admin.Pages.Common.Http;

public class PageResponse : IHasFactoryMethod<PageResponse, Page>
{
    [Required] public required int Id { get; set; }
    [Required] public required string Name { get; set; }
    [Required] public required string Title { get; set; }
    [Required] public required string Description { get; set; }
    [Required] public required string Slug { get; set; }
    public IList<SectionResponse> Sections { get; init; } = [];

    public static PageResponse CreateFrom(Page page)
    {
        var pageResponse = new PageResponse
        {
            Id = page.Id,
            Name = page.Name,
            Title = page.Title,
            Description = page.Description,
            Slug = page.Slug
        };

        foreach (var section in page.Sections)
        {
            pageResponse.Sections.Add(SectionResponse.CreateFrom(section));
        }

        return pageResponse;
    }
}