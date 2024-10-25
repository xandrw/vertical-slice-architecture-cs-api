using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.ChangePassword;

public class ChangePasswordRequest
{
    [Required(ErrorMessage = "error.password.required")]
    [MinLength(8, ErrorMessage = "error.password.tooShort")]
    [MaxLength(50, ErrorMessage = "error.password.tooLong")]
    public required string Password { get; set; }
}