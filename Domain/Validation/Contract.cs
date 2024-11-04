using System.Runtime.CompilerServices;

namespace Domain.Validation;

public static class Contract
{
    public static void Requires(bool condition, [CallerArgumentExpression(nameof(condition))] string message = "")
    {
        if (condition == false) throw new ArgumentException(message);
    }
    
    public static void RequiresNot(bool condition, [CallerArgumentExpression(nameof(condition))] string message = "")
    {
        if (condition) throw new ArgumentException(message);
    }
}