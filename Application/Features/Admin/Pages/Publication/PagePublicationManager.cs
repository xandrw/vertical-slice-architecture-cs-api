using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Pages;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.Publication;

public class PagePublicationManager(IDbProxy<Page> pagesProxy)
{
    public async Task PublishPageById(int id)
    {
        var page = await pagesProxy.Query().Include(p => p.Sections).SingleOrDefaultAsync(u => u.Id == id);

        if (page is null) throw new NotFoundHttpException<Page>();

        page.Publish();

        await pagesProxy.SaveChangesAsync();
    }

    public async Task UnpublishPageById(int id)
    {
        var page = await pagesProxy.Query().Include(p => p.Sections).SingleOrDefaultAsync(u => u.Id == id);

        if (page is null) throw new NotFoundHttpException<Page>();

        page.Unpublish();

        await pagesProxy.SaveChangesAsync();
    }
}