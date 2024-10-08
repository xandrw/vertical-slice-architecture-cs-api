using System.Net;

namespace Application.Common.Http.Exceptions;

public class UnauthorizedHttpException() : HttpException("Unauthorized")
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
}