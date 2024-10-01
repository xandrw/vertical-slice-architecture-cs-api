namespace Domain.Pages;

public class Section(string name, string value)
{
    public int Id { get; private set; }

    public string Name { get; private set; } = name;
    public string Value { get; private set; } = value;
}