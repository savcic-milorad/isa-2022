using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Security;
using TransfusionAPI.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TransfusionAPI.Application.TodoLists.Queries.GetTodos;

[Authorize]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;

    public GetTodosQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.TodoLists
                .Include(l => l.Items)
                .OrderBy(t => t.Title)
                .ToListAsync();
        var mappedList = list.Select(TodoListDto.From).ToList();

        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = mappedList
        };
    }
}
