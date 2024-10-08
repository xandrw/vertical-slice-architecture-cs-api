namespace Domain.Exceptions;

public class InvalidStringException(string? paramName) : ArgumentException(DefaultMessage, paramName)
{
    private static readonly string DefaultMessage = "Value cannot be null, empty or whitespace.";
}