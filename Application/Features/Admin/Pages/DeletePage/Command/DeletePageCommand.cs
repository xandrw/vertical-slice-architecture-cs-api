using Application.Common.Http.Exceptions;
using Application.Interfaces;
using Domain.Pages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.DeletePage.Command;

public record DeletePageCommand(int Id) : IRequest;

public class DeletePageCommandHandler(IRepository<Page> pagesRepository) : IRequestHandler<DeletePageCommand>
{
    public async Task Handle(DeletePageCommand command, CancellationToken cancellationToken)
    {
        var page = await pagesRepository.Query()
            .SingleOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

        if (page is null)
        {
            throw new NotFoundHttpException<Page>();
        }
        
        pagesRepository.Remove(page);
        await pagesRepository.SaveChangesAsync(cancellationToken);
    }
}