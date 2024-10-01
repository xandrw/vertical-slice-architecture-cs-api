using System.Net;

namespace Application.Features.Users;

public class UserNotFoundException : HttpException
{
    public UserNotFoundException() : base("user.not_found")
    {
    }
    
    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;
}