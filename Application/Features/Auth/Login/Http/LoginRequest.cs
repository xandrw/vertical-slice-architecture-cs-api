using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Features.Auth.Login.Http;

public class LoginRequest(string email, string password) : IRequest<LoginResponse>
{
    [Required]
    [EmailAddress]
    [MinLength(6)]
    public string Email { get; } = email;
    
    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    public string Password { get; } = password;
}