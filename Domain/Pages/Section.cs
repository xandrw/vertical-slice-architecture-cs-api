using Domain.Validation;

namespace Domain.Pages;

public class Section : BaseDomainEntity
{
    public int Id { get; private set; }

    public string Category { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    public static Section Create(string category, string name, string value)
    {
        Contract.Requires(!string.IsNullOrWhiteSpace(category) && category.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(value));
        
        return new Section { Category = category, Name = name, Value = value };
    }
}