using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Pages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.DeletePage.Command;

public class DeletePageCommand(int id) : IRequest
{
    public int Id { get; } = id;
}

public class DeletePageCommandHandler(IDataProxy<Page> pagesProxy) : IRequestHandler<DeletePageCommand>
{
    public async Task Handle(DeletePageCommand command, CancellationToken cancellationToken)
    {
        var page = await pagesProxy.Query()
            .SingleOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

        if (page is null)
        {
            throw new NotFoundHttpException<Page>();
        }
        
        pagesProxy.Remove(page);
        await pagesProxy.SaveChangesAsync(cancellationToken);
    }
}