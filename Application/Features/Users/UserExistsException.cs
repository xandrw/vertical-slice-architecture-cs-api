using System.Net;

namespace Application.Features.Users;

public class UserExistsException : HttpException
{
    public UserExistsException() : base("user.exists")
    {
    }

    public UserExistsException(string message) : base(message)
    {
    }

    public UserExistsException(string message, Exception inner) : base(message, inner)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Conflict;
}