using System.ComponentModel.DataAnnotations;
using Application.Common.Http.Responses;
using Domain.Users;

namespace Application.Features.Admin.Users.Common;

public class UserResponse : IHasFactoryMethod<UserResponse, User>
{
    [Required] public required int Id { get; set; }
    [Required] public required string Email { get; set; }
    [Required] public required string Role { get; set; }

    public static UserResponse CreateFrom(User user) => new() { Id = user.Id, Email = user.Email, Role = user.Role };
}