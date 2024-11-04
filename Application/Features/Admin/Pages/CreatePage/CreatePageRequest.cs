using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Pages.CreatePage;

public class CreatePageRequest(string name, string title, string description)
{
    [Required(ErrorMessage = "error.name.required")]
    [MinLength(3, ErrorMessage = "error.name.min_length")]
    [MaxLength(30, ErrorMessage = "error.name.max_length")]
    public string Name { get; } = name;

    [Required(ErrorMessage = "error.title.required")]
    [MinLength(3, ErrorMessage = "error.title.min_length")]
    [MaxLength(60, ErrorMessage = "error.title.max_length")]
    public string Title { get; } = title;

    [Required(ErrorMessage = "error.name.required")]
    [MinLength(3, ErrorMessage = "error.name.min_length")]
    public string Description { get; } = description;

    public IList<CreatePageRequestSectionItem> Sections { get; init; } = [];
}