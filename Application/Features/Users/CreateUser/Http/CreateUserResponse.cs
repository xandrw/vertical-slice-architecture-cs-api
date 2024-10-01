using System.ComponentModel.DataAnnotations;

namespace Application.Features.Users.CreateUser.Http;

public class CreateUserResponse
{
    [Required] public required int Id { get; set; }

    [Required] public required string Email { get; set; }

    [Required] public required string Role { get; set; }
}