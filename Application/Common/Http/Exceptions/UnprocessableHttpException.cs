using System.Net;

namespace Application.Common.Http.Exceptions;

public class UnprocessableHttpException(string message, Dictionary<string, string[]?> errors) : HttpException(message)
{
    public Dictionary<string, string[]?> Errors { get; } = errors;

    public override int StatusCode => (int)HttpStatusCode.UnprocessableContent;
}