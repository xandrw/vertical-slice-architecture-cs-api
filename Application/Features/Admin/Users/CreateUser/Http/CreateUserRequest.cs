using System.ComponentModel.DataAnnotations;
using Domain.Users;
using MediatR;

namespace Application.Features.Admin.Users.CreateUser.Http;

public class CreateUserRequest(string email, string password, string role) : IRequest<User>
{
    [Required]
    [EmailAddress]
    [MinLength(7)]
    public string Email { get; } = email;

    [Required]
    [MinLength(8)]
    [MaxLength(50)]
    public string Password { get; } = password;
    
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    [RegularExpression("^(Admin|Author)$")]
    public string Role { get; } = role;
}