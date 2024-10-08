using System.Net;

namespace Application.Common.Http.Exceptions;

public class NotFoundHttpException<T>() : HttpException($"{typeof(T).Name.ToLowerInvariant()}_not_found")
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;
}