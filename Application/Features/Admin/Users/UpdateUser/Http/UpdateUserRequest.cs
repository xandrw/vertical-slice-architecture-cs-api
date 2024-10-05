using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Users.UpdateUser.Http;

public class UpdateUserRequest
{
    [Required]
    [EmailAddress]
    [MinLength(7)]
    public required string Email { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    [RegularExpression("^(Admin|Author)$")]
    public required string Role { get; set; }
}