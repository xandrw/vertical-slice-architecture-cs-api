using MediatR;

namespace Application.Features.Auth.ChangePassword.Command;

public class ChangePasswordCommand(int id, string password) : IRequest
{
    public int Id { get; } = id;
    public string Password { get; } = password;
}