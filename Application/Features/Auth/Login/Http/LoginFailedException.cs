using System.Net;

namespace Application.Features.Auth.Login.Http;

public class LoginFailedException : HttpException
{
    public LoginFailedException() : base("login.failed")
    {
    }

    public LoginFailedException(string message) : base(message)
    {
    }

    public LoginFailedException(string message, Exception inner) : base(message, inner)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
}