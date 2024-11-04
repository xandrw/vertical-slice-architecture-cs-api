using Application.Interfaces;
using Domain.Pages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Pages.ListPages.Command;

public record ListPagesCommand(int PageNumber = 1, int PageSize = 10) : IRequest<(int total, IList<Page> pages)>;

public class ListPagesCommandHandler(IRepository<Page> pagesRepository)
    : IRequestHandler<ListPagesCommand, (int total, IList<Page> pages)>
{
    public async Task<(int, IList<Page>)> Handle(ListPagesCommand command, CancellationToken cancellationToken)
    {
        var total = await pagesRepository.Query().CountAsync(cancellationToken);
        var pages = await pagesRepository.Query()
            .Include(p => p.Sections)
            .Skip((command.PageNumber - 1) * command.PageSize)
            .Take(command.PageSize)
            .ToListAsync(cancellationToken);

        return (total, pages);
    }
}