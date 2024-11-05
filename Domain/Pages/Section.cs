using Domain.Validation;

namespace Domain.Pages;

public class Section : BaseDomainEntity
{
    public string Category { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    public static Section Create(string category, string name, string value, int id = 0)
    {
        Contract.Requires(!string.IsNullOrWhiteSpace(category) && category.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(value));

        return new Section { Category = category, Name = name, Value = value, Id = id };
    }

    public Section Update(string category, string name, string value)
    {
        Contract.Requires(!string.IsNullOrWhiteSpace(category) && category.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(value));

        Category = category;
        Name = name;
        Value = value;

        return this;
    }
}