using System.ComponentModel.DataAnnotations;
using Domain.Users;
using MediatR;

namespace Application.Features.Admin.Users.CreateUser.Command;

public class CreateUserRequest(string email, string role, string password) : IRequest<User>
{
    [Required(ErrorMessage = "error.email.required")]
    [EmailAddress(ErrorMessage = "error.email.invalid")]
    [MinLength(6, ErrorMessage = "error.email.min_length")]
    [MaxLength(60, ErrorMessage = "error.email.max_length")]
    public string Email { get; } = email;

    [Required(ErrorMessage = "error.role.required")]
    [AllowedValues(Domain.Users.Role.Author, Domain.Users.Role.Admin, ErrorMessage = "error.role.invalid")]
    public string Role { get; } = role;
    
    [Required(ErrorMessage = "error.password.required")]
    [MinLength(8, ErrorMessage = "error.password.min_length")]
    [MaxLength(60, ErrorMessage = "error.password.max_length")]
    public string Password { get; } = password;
}