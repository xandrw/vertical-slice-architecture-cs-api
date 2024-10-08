using Domain.Exceptions;

namespace Domain.Pages;

public class Section : BaseEntity
{
    public int Id { get; private set; }

    public string Category { get; private set; } = "";
    public string Name { get; private set; } = "";
    public string Value { get; private set; } = "";

    public static Section Create(string category, string name, string value)
    {
        if (string.IsNullOrWhiteSpace(category) || category.Length == 0)
        {
            throw new InvalidStringException(nameof(category));
        }

        if (string.IsNullOrWhiteSpace(name) || name.Length == 0)
        {
            throw new InvalidStringException(nameof(name));
        }
        
        if (string.IsNullOrWhiteSpace(value) || value.Length == 0)
        {
            throw new InvalidStringException(nameof(value));
        }
        
        return new Section { Category = category, Name = name, Value = value };
    }
}