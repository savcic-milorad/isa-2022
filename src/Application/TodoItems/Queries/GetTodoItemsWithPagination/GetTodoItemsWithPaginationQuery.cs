using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Mappings;
using TransfusionAPI.Application.Common.Models;
using MediatR;

namespace TransfusionAPI.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await _context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .OrderBy(x => x.Title)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        var mappedPaginatedList = paginatedList.PaginatedListMap(TodoItemBriefDto.From);

        return mappedPaginatedList;
    }
}
