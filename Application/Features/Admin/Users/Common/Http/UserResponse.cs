using System.ComponentModel.DataAnnotations;

namespace Application.Features.Admin.Users.Common.Http;

public class UserResponse
{
    [Required] public required int Id { get; set; }

    [Required] public required string Email { get; set; }

    [Required] public required string Role { get; set; }
}