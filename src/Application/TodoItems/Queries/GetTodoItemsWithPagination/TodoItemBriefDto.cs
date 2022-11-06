using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }

    public static TodoItemBriefDto From(TodoItem ti)
    {
        return new TodoItemBriefDto()
        {
            Done = ti.Done,
            Id = ti.Id,
            Title = ti.Title,
            ListId = ti.ListId
        };
    }
}
