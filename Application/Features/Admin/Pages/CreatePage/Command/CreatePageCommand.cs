using Application.Common.Http.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Domain.Pages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.CreatePage.Command;

public record CreatePageCommand(
    string Name,
    string Title,
    string Description,
    IList<CreatePageCommandSectionItem> Sections) : IRequest<Page>
{
    public static CreatePageCommand CreateFrom(CreatePageRequest request)
    {
        var sections = new List<CreatePageCommandSectionItem>();
        
        foreach (var section in request.Sections)
        {
            sections.Add(new CreatePageCommandSectionItem(section.Category, section.Name, section.Value));
        }
        
        return new CreatePageCommand(request.Name, request.Title, request.Description, sections);
    }
}

public record CreatePageCommandSectionItem(string Category, string Name, string Value);

public class CreatePageCommandHandler(IRepository<Page> pagesRepository) : IRequestHandler<CreatePageCommand, Page>
{
    public async Task<Page> Handle(CreatePageCommand command, CancellationToken cancellationToken)
    {
        var page = Page.Create(command.Name, command.Title, command.Description);

        foreach (var section in command.Sections)
        {
            page.AddSection(Section.Create(section.Category, section.Name, section.Value));
        }

        try
        {
            pagesRepository.Add(page);
            await pagesRepository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<Page>();
        }

        return page;
    }
}