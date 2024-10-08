using System.Net;

namespace Application;

public class HttpException(string message) : Exception(message)
{
    public virtual int StatusCode => (int)HttpStatusCode.InternalServerError;
}