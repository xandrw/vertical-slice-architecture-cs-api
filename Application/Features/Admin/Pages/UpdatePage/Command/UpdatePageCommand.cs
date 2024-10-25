using System.Collections.Immutable;
using Application.Common.Http.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Domain.Pages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.UpdatePage.Command;

public class UpdatePageCommand(
    int id,
    string name,
    string title,
    string description,
    IList<UpdatePageRequestSectionItem> sections
) : IRequest<Page>
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Title { get; } = title;
    public string Description { get; } = description;
    public IList<UpdatePageRequestSectionItem> Sections { get; } = sections;
}

public class UpdatePageCommandHandler(IDbProxy<Page> pagesProxy) : IRequestHandler<UpdatePageCommand, Page>
{
    public async Task<Page> Handle(UpdatePageCommand command, CancellationToken cancellationToken)
    {
        var page = await pagesProxy.Query()
            .Include(p => p.Sections)
            .SingleOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

        if (page is null) throw new NotFoundHttpException<Page>();

        page.Update(command.Name, command.Title, command.Description);

        var sectionsToAdd = command.Sections
            .Where(cs => cs.Id is 0)
            .Select(cs => Section.Create(cs.Category, cs.Name, cs.Value))
            .ToImmutableList();

        var sectionsToUpdate = command.Sections
            .Where(cs => page.Sections.Any(ps => ps.Id == cs.Id))
            .Select(cs => Section.Create(cs.Category, cs.Name, cs.Value, cs.Id))
            .Except(sectionsToAdd)
            .ToImmutableList();

        var sectionsToRemove = page.Sections
            .Where(ps => command.Sections.All(cs => cs.Id != ps.Id))
            .Except(sectionsToAdd)
            .Except(sectionsToUpdate)
            .ToImmutableList();

        page.AddSections(sectionsToAdd)
            .UpdateSections(sectionsToUpdate)
            .RemoveSections(sectionsToRemove);

        try
        {
            await pagesProxy.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<Page>();
        }

        return page;
    }
}