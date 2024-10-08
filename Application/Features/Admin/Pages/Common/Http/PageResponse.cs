using System.ComponentModel.DataAnnotations;
using Domain.Pages;

namespace Application.Features.Admin.Pages.Common.Http;

public class PageResponse
{
    [Required] public required int Id { get; set; }
    [Required] public required string Name { get; set; }
    [Required] public required string Title { get; set; }
    [Required] public required string Description { get; set; }
    [Required] public required string Slug { get; set; }
    public IList<SectionResponse> Sections { get; set; } = [];

    public static PageResponse CreateFromEntity(Page page)
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
            pageResponse.Sections.Add(new SectionResponse
            {
                Id = section.Id,
                Category = section.Category,
                Name = section.Name,
                Value = section.Value
            });
        }

        return pageResponse;
    }
}