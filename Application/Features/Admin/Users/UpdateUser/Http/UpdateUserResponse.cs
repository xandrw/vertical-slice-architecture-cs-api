using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Users.UpdateUser.Http;

public class UpdateUserResponse
{
    [Required] public required int Id { get; set; }

    [Required] public required string Email { get; set; }

    [Required] public required string Role { get; set; }
}