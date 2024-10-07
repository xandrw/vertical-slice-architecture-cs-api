using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Extensions;

public static class IndexBuilderExtensions
{
    public static IndexBuilder<T> IsUniqueWithPrefix<T>(this IndexBuilder<T> indexBuilder, bool unique = true)
    {
        indexBuilder.IsUnique(unique);
        string? defaultIndexName = indexBuilder.Metadata.GetDefaultDatabaseName();

        if (defaultIndexName is null)
        {
            var propertyNames = string.Join('_', indexBuilder.Metadata.Properties.Select(p => p.Name));
            
            throw new EntityConfigurationException(
                $"Unable to generate default index name for {typeof(T).Name}_{propertyNames}." +
                $"Ensure the index is properly configured.");
        }
        
        indexBuilder.HasDatabaseName($"UQ_{defaultIndexName}");

        return indexBuilder;
    }
}