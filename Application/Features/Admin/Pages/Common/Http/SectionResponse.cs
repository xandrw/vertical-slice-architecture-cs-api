using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Pages.Common.Http;

public class SectionResponse
{
    [Required] public required int Id { get; set; }
    [Required] public required string Category { get; set; }
    [Required] public required string Name { get; set; }
    [Required] public required string Value { get; set; }
}