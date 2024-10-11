using Application.Common.Http.Responses;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Application.Common.Http.Swagger;

public class UnprocessableResponseExample : IExamplesProvider<UnprocessableEntityResponse>
{
    public UnprocessableEntityResponse GetExamples()
    {
        return new UnprocessableEntityResponse
        {
            Error = "Validation Failed",
            Status = StatusCodes.Status422UnprocessableEntity,
            Errors = new Dictionary<string, string[]?>
            {
                { "field", ["error.field.rule"] }
            }
        };
    }
}