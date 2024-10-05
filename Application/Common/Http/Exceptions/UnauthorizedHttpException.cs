namespace Application.Common.Http.Exceptions;

public class UnauthorizedHttpException : HttpException
{
    public UnauthorizedHttpException() : base("unauthorized")
    {
    }
    
    public UnauthorizedHttpException(string message) : base(message)
    {
    }

    public UnauthorizedHttpException(string message, Exception inner) : base(message, inner)
    {
    }
}