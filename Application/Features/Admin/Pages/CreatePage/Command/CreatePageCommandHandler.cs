using Application.Common.Http.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Domain.Pages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.CreatePage.Command;

public class CreatePageCommandHandler(IDataProxy<Page> pagesProxy) : IRequestHandler<CreatePageRequest, Page>
{
    public async Task<Page> Handle(CreatePageRequest request, CancellationToken cancellationToken)
    {
        var page = Page.Create(request.Name, request.Title, request.Description);

        foreach (var section in request.Sections)
        {
            page.AddSection(Section.Create(section.Category, section.Name, section.Value));
        }

        try
        {
            pagesProxy.Add(page);
            await pagesProxy.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e) when (e.HasUniqueConstraintError())
        {
            throw new ConflictHttpException<Page>();
        }

        return page;
    }
}