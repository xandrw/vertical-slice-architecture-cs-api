namespace Domain.Pages;

public class Page(string name, string title, string description)
{
    public int Id { get; private set; }

    public string Name { get; private set; } = name;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public IList<Section> Sections { get; private set; } = [];
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? PublishedAt { get; private set; }

    // TODO: move to Response class
    public string Slug => Title.ToLower().Replace(' ', '-');

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
}