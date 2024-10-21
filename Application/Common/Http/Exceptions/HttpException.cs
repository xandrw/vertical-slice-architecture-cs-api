using System.Net;

namespace Application.Common.Http.Exceptions;

public class HttpException(string message) : Exception(message)
{
    public virtual int StatusCode => (int)HttpStatusCode.InternalServerError;
}