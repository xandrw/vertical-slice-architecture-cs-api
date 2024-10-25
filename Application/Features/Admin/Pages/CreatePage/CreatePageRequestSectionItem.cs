using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Pages.CreatePage;

public class CreatePageRequestSectionItem(string category, string name, string value)
{
    [Required(ErrorMessage = "error.category.required")]
    [MinLength(3, ErrorMessage = "error.category.min_length")]
    public string Category { get; } = category;

    [Required(ErrorMessage = "error.name.required")]
    [MinLength(3, ErrorMessage = "error.name.min_length")]
    public string Name { get; } = name;

    [Required(ErrorMessage = "error.value.required")]
    public string Value { get; } = value;
}