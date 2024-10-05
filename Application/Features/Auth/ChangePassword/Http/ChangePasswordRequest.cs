using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.ChangePassword.Http;

public class ChangePasswordRequest
{
    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    public required string Password { get; set; }
}