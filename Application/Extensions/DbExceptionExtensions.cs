using Microsoft.EntityFrameworkCore;

namespace Application.Extensions;

public static class DbExceptionExtensions
{
    public static bool HasUniqueConstraintError(this DbUpdateException e)
    {
        return e.InnerException is not null &&
               (e.InnerException.Message.Contains("uq", StringComparison.InvariantCultureIgnoreCase) ||
                e.InnerException.Message.Contains("unique", StringComparison.InvariantCultureIgnoreCase) ||
                e.InnerException.Message.Contains("duplicate", StringComparison.InvariantCultureIgnoreCase));
    }
}