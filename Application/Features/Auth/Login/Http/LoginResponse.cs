using System.ComponentModel.DataAnnotations;

namespace Application.Features.Auth.Login.Http;

public class LoginResponse
{
    [Required] public required string Token { get; set; }
    [Required] public required LoginUserResponse User { get; set; }

    public class LoginUserResponse
    {
        [Required] public required int Id { get; set; }

        [Required] public required string Email { get; set; }

        [Required] public required string Role { get; set; }
    }
}