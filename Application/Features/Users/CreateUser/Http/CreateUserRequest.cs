using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.CreateUser.Http;

public class CreateUserRequest
{
    [Required]
    [EmailAddress]
    [MinLength(7)]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    public required string Password { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    [RegularExpression("^(Admin|Author)$")]
    public required string Role { get; set; }
}