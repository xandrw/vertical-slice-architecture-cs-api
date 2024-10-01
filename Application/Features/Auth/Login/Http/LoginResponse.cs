using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.Login.Http;

public class LoginResponse
{
    [Required] public required string Token { get; set; }
}