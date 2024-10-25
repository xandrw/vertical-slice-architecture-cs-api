using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Features.Auth.Login;

public class LoginRequest(string email, string password) : IRequest<LoginResponse>
{
    [Required(ErrorMessage = "error.email.required")]
    [EmailAddress(ErrorMessage = "error.email.invalid")]
    [MinLength(6, ErrorMessage = "error.email.tooShort")]
    public string Email { get; } = email;
    
    [Required(ErrorMessage = "error.password.required")]
    [MinLength(8, ErrorMessage = "error.password.tooShort")]
    [MaxLength(50, ErrorMessage = "error.password.tooLong")]
    public string Password { get; } = password;
}