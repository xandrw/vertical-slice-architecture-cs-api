using System.Collections.Immutable;
using Domain.Validation;

namespace Domain.Pages;

public class Page : BaseDomainEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private readonly IList<Section> _sections = new List<Section>();
    public IReadOnlyList<Section> Sections => _sections.ToList().AsReadOnly();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; private set; }

    public string Slug => Title.ToLower().Replace(' ', '-');

    public static Page Create(string name, string title, string description)
    {
        Contract.Requires(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(title) && title.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(description) && description.Length > 0);

        return new Page { Name = name, Title = title, Description = description };
    }

    public Page Update(string name, string title, string description)
    {
        Contract.Requires(!string.IsNullOrWhiteSpace(name) && name.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(title) && title.Length > 0);
        Contract.Requires(!string.IsNullOrWhiteSpace(description) && description.Length > 0);

        Name = name;
        Title = title;
        Description = description;

        return this;
    }

    public Page Publish()
    {
        if (PublishedAt is not null) return this;
        PublishedAt = DateTime.UtcNow;
        return this;
    }

    public Page Unpublish()
    {
        if (PublishedAt is null) return this;
        PublishedAt = null;
        return this;
    }

    public Page AddSection(Section section)
    {
        Contract.Requires(Sections.All(s => s.Name != section.Name), "error.section.exists");
        _sections.Add(section);
        return this;
    }

    public Page AddSections(IImmutableList<Section> sections)
    {
        foreach (Section section in sections)
        {
            AddSection(section);
        }

        return this;
    }

    public Page UpdateSections(IImmutableList<Section> sections)
    {
        foreach (Section section in sections)
        {
            Contract.Requires(!Sections.Any(s => s.Name == section.Name && s.Id != section.Id), "error.section.exists");
            _sections.First(s => s.Id == section.Id).Update(section.Category, section.Name, section.Value);
        }

        return this;
    }

    public Page RemoveSection(Section section)
    {
        _sections.Remove(section);
        return this;
    }

    public Page RemoveSections(IImmutableList<Section> sections)
    {
        foreach (Section section in sections)
        {
            RemoveSection(section);
        }

        return this;
    }
}