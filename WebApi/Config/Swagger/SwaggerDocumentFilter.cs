using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Config.Swagger;

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
    {
        var sortedPaths = new OpenApiPaths();
        var authPaths = openApiDocument.Paths
            .Where(path => path.Value.Operations.Any(op => op.Value.Tags.Any(tag => tag.Name == "Auth")))
            .OrderBy(pair => pair.Key);

        foreach (var path in authPaths)
        {
            sortedPaths.Add(path.Key, path.Value);
        }

        var otherPaths = openApiDocument.Paths
            .Where(path => path.Value.Operations.All(op => op.Value.Tags.All(tag => tag.Name != "Auth")))
            .OrderBy(pair => pair.Key);

        foreach (var path in otherPaths)
        {
            sortedPaths.Add(path.Key, path.Value);
        }

        openApiDocument.Paths = sortedPaths;

        foreach (var pathPair in openApiDocument.Paths)
        {
            var sortedOperations = pathPair.Value.Operations
                .OrderBy(pair => GetOperationOrder(pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            pathPair.Value.Operations = sortedOperations;

            foreach (var operation in pathPair.Value.Operations)
            {
                operation.Value.Tags = operation.Value.Tags
                    .OrderBy(tag => tag.Name == "Auth" ? 0 : 1)
                    .ThenBy(tag => tag.Name)
                    .ToList();
            }
        }
    }

    private int GetOperationOrder(OperationType operationType) => operationType switch
    {
        OperationType.Get => 10,
        OperationType.Post => 20,
        OperationType.Put => 30,
        OperationType.Patch => 40,
        OperationType.Delete => 50,
        _ => 50
    };
}