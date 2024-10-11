using Application.Common.Http.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class ValidateModelFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        
        var errors = context.ModelState
            .Where(predicate => predicate.Value is not null && predicate.Value.Errors.Any())
            .ToDictionary(
                kvp => kvp.Key.ToLowerInvariant(),
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        throw new UnprocessableHttpException("Validation Failed", errors);
    }
}