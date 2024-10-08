using System.Net;

namespace Application.Common.Http.Exceptions;

public class ConflictHttpException<T>() : HttpException($"{typeof(T).Name.ToLowerInvariant()}.exists")
{
    public override int StatusCode => (int)HttpStatusCode.Conflict;
}