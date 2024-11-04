using System.Collections.Immutable;
using Domain.Pages;

namespace Unit.Domain.Pages;

[TestFixture]
public class PageTest
{
    [Test]
    public void CreatePage()
    {
        const string name = "Page Name";
        const string title = "Page Title";
        const string slug = "page-title";
        const string description = "Page Description";

        var page = Page.Create(name, title, description);

        Assert.That(page.Id, Is.EqualTo(0));
        Assert.That(page.Name, Is.EqualTo(name));
        Assert.That(page.Title, Is.EqualTo(title));
        Assert.That(page.Slug, Is.EqualTo(slug));
        Assert.That(page.Description, Is.EqualTo(description));
        Assert.That(page.Sections, Is.Empty);
    }

    [Test]
    [TestCase(null, "Title", "Description", "name", TestName = "Null name")]
    [TestCase("Name", null, "Description", "title", TestName = "Null title")]
    [TestCase("Name", "Title", null, "description", TestName = "Null description")]
    [TestCase("", "Title", "Description", "name", TestName = "Empty name")]
    [TestCase("Name", "", "Description", "title", TestName = "Empty title")]
    [TestCase("Name", "Title", "", "description", TestName = "Empty description")]
    public void CreateInvalidPage(string? name, string? title, string? description, string field)
    {
        var exception = Assert.Throws<ArgumentException>(() => Page.Create(name!, title!, description!));

        Assert.That(exception.Message, Contains.Substring(field));
    }

    [Test]
    public void UpdatePage()
    {
        var page = Page.Create("Initial Name", "Initial Title", "Initial Description");

        const string name = "Page Name";
        const string title = "Page Title";
        const string slug = "page-title";
        const string description = "Page Description";

        page.Update(name, title, description);


        Assert.That(page.Name, Is.EqualTo(name));
        Assert.That(page.Title, Is.EqualTo(title));
        Assert.That(page.Slug, Is.EqualTo(slug));
        Assert.That(page.Description, Is.EqualTo(description));
        Assert.That(page.Sections, Is.Empty);
    }

    [Test]
    [TestCase(null, "Title", "Description", "name", TestName = "Null name")]
    [TestCase("Name", null, "Description", "title", TestName = "Null title")]
    [TestCase("Name", "Title", null, "description", TestName = "Null description")]
    [TestCase("", "Title", "Description", "name", TestName = "Empty name")]
    [TestCase("Name", "", "Description", "title", TestName = "Empty title")]
    [TestCase("Name", "Title", "", "description", TestName = "Empty description")]
    public void UpdateInvalidPage(string? name, string? title, string? description, string field)
    {
        var page = Page.Create("Initial Name", "Initial Title", "Initial Description");
        var exception = Assert.Throws<ArgumentException>(() => page.Update(name!, title!, description!));

        Assert.That(exception.Message, Contains.Substring(field));
    }

    [Test]
    public void Publish()
    {
        var page = Page.Create("Name", "Title", "Description");
        page.Publish();

        Assert.That(page.PublishedAt, Is.Not.Null);
    }

    [Test]
    public void Unpublish()
    {
        var page = Page.Create("Name", "Title", "Description");
        page.Publish()
            .Unpublish();

        Assert.That(page.PublishedAt, Is.Null);
    }

    [Test]
    public void AddSection()
    {
        var page = Page.Create("Name", "Title", "Description");
        var section = Section.Create("Category", "Name", "Value", 1);
        page.AddSection(section);

        Assert.That(page.Sections, Is.Not.Empty);
        Assert.That(page.Sections[0], Is.EqualTo(section));
    }

    [Test]
    public void DisallowAddDuplicateSection()
    {
        var page = Page.Create("Name", "Title", "Description");
        var section = Section.Create("Category", "Name", "Value", 1);
        page.AddSection(section);

        var exception = Assert.Throws<ArgumentException>(() => page.AddSection(section));

        Assert.That(exception.Message, Contains.Substring("error.section.exists"));
    }

    [Test]
    public void AddSections()
    {
        var page = Page.Create("Name", "Title", "Description");
        IList<Section> sections =
        [
            Section.Create("Category", "Name", "Value", 1),
            Section.Create("Category", "Name2", "Value", 2)
        ];
        page.AddSections(sections.ToImmutableList());

        Assert.That(page.Sections.Count, Is.EqualTo(2));
        Assert.That(page.Sections[0], Is.EqualTo(sections[0]));
        Assert.That(page.Sections[1], Is.EqualTo(sections[1]));
    }

    [Test]
    public void RemoveSection()
    {
        var page = Page.Create("Name", "Title", "Description");
        var section = Section.Create("Category", "Name", "Value", 1);
        page.AddSection(section);
        page.RemoveSection(section);

        Assert.That(page.Sections, Is.Empty);
    }

    [Test]
    public void RemoveSections()
    {
        var page = Page.Create("Name", "Title", "Description");
        IList<Section> sections =
        [
            Section.Create("Category", "Name", "Value", 1),
            Section.Create("Category", "Name 2", "Value", 2)
        ];
        page.AddSections(sections.ToImmutableList());
        page.RemoveSections(sections.ToImmutableList());

        Assert.That(page.Sections, Is.Empty);
    }

    [Test]
    public void UpdateSection()
    {
        var page = Page.Create("Name", "Title", "Description");
        IList<Section> sections =
        [
            Section.Create("Category", "Name", "Value", 1),
            Section.Create("Category", "Name 2", "Value", 2)
        ];
        IList<Section> updatedSections =
        [
            Section.Create("u:Category", "Name", "u:Value", 1),
            Section.Create("u:Category", "u:Name 2", "u:Value", 2)
        ];
        page.AddSections(sections.ToImmutableList());
        page.UpdateSections(updatedSections.ToImmutableList());

        Assert.That(page.Sections.Count, Is.EqualTo(2));
        Assert.That(page.Sections[0].Name, Is.EqualTo(updatedSections[0].Name));
        Assert.That(page.Sections[0].Category, Is.EqualTo(updatedSections[0].Category));
        Assert.That(page.Sections[0].Value, Is.EqualTo(updatedSections[0].Value));

        Assert.That(page.Sections[1].Name, Is.EqualTo(updatedSections[1].Name));
        Assert.That(page.Sections[1].Category, Is.EqualTo(updatedSections[1].Category));
        Assert.That(page.Sections[1].Value, Is.EqualTo(updatedSections[1].Value));
    }

    [Test]
    public void DisallowUpdateDuplicateSection()
    {
        var page = Page.Create("Name", "Title", "Description");
        IList<Section> sections =
        [
            Section.Create("Category", "Name", "Value", 1),
            Section.Create("Category", "Name 2", "Value", 2)
        ];
        IList<Section> updatedSections = [Section.Create("u:Category", "Name", "u:Value", 2)];
        page.AddSections(sections.ToImmutableList());

        var exception = Assert.Throws<ArgumentException>(() => page.UpdateSections(updatedSections.ToImmutableList()));

        Assert.That(exception.Message, Contains.Substring("error.section.exists"));
    }
}