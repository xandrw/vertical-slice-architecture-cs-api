using System.ComponentModel.DataAnnotations;
using Application.Common.Http.Responses;
using Domain.Pages;

namespace Application.Features.Admin.Pages.Common;

public class SectionResponse : IHasFactoryMethod<SectionResponse, Section>
{
    [Required] public required int Id { get; set; }
    [Required] public required string Category { get; set; }
    [Required] public required string Name { get; set; }
    [Required] public required string Value { get; set; }
    
    public static SectionResponse CreateFrom(Section section)
    {
        return new()
        {
            Id = section.Id,
            Category = section.Category,
            Name = section.Name,
            Value = section.Value
        };
    }
}