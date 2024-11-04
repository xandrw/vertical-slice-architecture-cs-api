using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Pages.UpdatePage;

public class UpdatePageRequest(string name, string title, string description)
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

    public IList<UpdatePageRequestSectionItem> Sections { get; init; } = new List<UpdatePageRequestSectionItem>();
}

public class UpdatePageRequestSectionItem(string category, string name, string value)
{
    [Range(1, int.MaxValue, ErrorMessage = "error.id.invalid")]
    public int? Id { get; init; }
    
    [Required(ErrorMessage = "error.category.required")]
    [MinLength(3, ErrorMessage = "error.category.min_length")]
    public string Category { get; } = category;

    [Required(ErrorMessage = "error.name.required")]
    [MinLength(3, ErrorMessage = "error.name.min_length")]
    public string Name { get; } = name;

    [Required(ErrorMessage = "error.value.required")]
    public string Value { get; } = value;
}