using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Users.CreateUser.Command;

public class CreateUserResponse
{
    [Required] public required int Id { get; set; }

    [Required] public required string Email { get; set; }

    [Required] public required string Role { get; set; }
}