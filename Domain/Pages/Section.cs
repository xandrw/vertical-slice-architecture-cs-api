namespace Domain.Pages;

public class Section(string name, string value) : IEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = name;
    public string Value { get; private set; } = value;
}