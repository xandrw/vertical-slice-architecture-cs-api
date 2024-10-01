using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.Login.Http;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    [MinLength(7)]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    public required string Password { get; set; }
}