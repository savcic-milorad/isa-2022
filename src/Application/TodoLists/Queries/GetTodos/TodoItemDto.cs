using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.TodoLists.Queries.GetTodos;

public class TodoItemDto
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }

    public int Priority { get; set; }

    public string? Note { get; set; }

    public static TodoItemDto From(TodoItem todoItem)
    {
        return new TodoItemDto()
        {
            Id = todoItem.Id,
            Title = todoItem.Title,
            Done = todoItem.Done,
            Priority = (int)todoItem.Priority,
            Note = todoItem.Note,
            ListId = todoItem.ListId
        };
    }
}
