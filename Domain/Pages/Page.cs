using Domain.Exceptions;

namespace Domain.Pages;

public class Page : BaseEntity
{
    public int Id { get; private set; }

    public string Name { get; private set; } = "";
    public string Title { get; private set; } = "";
    public string Description { get; private set; } = "";

    private readonly IList<Section> _sections = new List<Section>();
    public IReadOnlyList<Section> Sections => _sections.ToList().AsReadOnly();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; private set; }

    public string Slug => Title.ToLower().Replace(' ', '-');

    public static Page Create(string name, string title, string description)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length == 0)
        {
            throw new InvalidStringException(nameof(name));
        }

        if (string.IsNullOrWhiteSpace(title) || title.Length == 0)
        {
            throw new InvalidStringException(nameof(title));
        }

        if (string.IsNullOrWhiteSpace(description) || description.Length == 0)
        {
            throw new InvalidStringException(nameof(description));
        }

        return new Page { Name = name, Title = title, Description = description };
    }

    public Page Publish()
    {
        PublishedAt = DateTime.UtcNow;
        return this;
    }

    public Page Unpublish()
    {
        PublishedAt = null;
        return this;
    }

    public Page AddSection(Section section)
    {
        _sections.Add(section);
        return this;
    }

    public Page RemoveSection(Section section)
    {
        _sections.Remove(section);
        return this;
    }
}