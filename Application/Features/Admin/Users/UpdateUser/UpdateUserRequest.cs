using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Users.UpdateUser;

public class UpdateUserRequest
{
    [Required(ErrorMessage = "error.email.required")]
    [EmailAddress(ErrorMessage = "error.email.invalid")]
    [MinLength(6, ErrorMessage = "error.email.min_length")]
    [MaxLength(60, ErrorMessage = "error.email.max_length")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "error.role.required")]
    [AllowedValues(Domain.Users.Role.Author, Domain.Users.Role.Admin, ErrorMessage = "error.role.invalid")]
    public required string Role { get; set; }
}