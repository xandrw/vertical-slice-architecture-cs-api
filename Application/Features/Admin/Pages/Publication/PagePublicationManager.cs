using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Pages;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.Publication;

public class PagePublicationManager(IRepository<Page> pagesRepository)
{
    public async Task<DateTime?> PublishPageById(int id)
    {
        var page = await pagesRepository.Query().SingleOrDefaultAsync(u => u.Id == id);

        if (page is null) throw new NotFoundHttpException<Page>();

        page.Publish();

        await pagesRepository.SaveChangesAsync();

        return page.PublishedAt;
    }

    public async Task UnpublishPageById(int id)
    {
        var page = await pagesRepository.Query().SingleOrDefaultAsync(u => u.Id == id);

        if (page is null) throw new NotFoundHttpException<Page>();

        page.Unpublish();

        await pagesRepository.SaveChangesAsync();
    }
}