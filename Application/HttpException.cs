using System.Net;

namespace Application;

public class HttpException : Exception
{
    public HttpException(string message) : base(message)
    {
    }

    public HttpException(string message, Exception inner) : base(message, inner)
    {
    }

    public virtual int StatusCode => (int)HttpStatusCode.InternalServerError;
}