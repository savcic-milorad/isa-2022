using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.TodoLists.Queries.GetTodos;

public class TodoListDto
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Colour { get; set; }

    public IList<TodoItemDto> Items { get; set; }

    public static TodoListDto From(TodoList todoList)
    {
        return new TodoListDto
        {
            Id = todoList.Id,
            Title = todoList.Title,
            Colour = todoList.Colour,
            Items = todoList.Items.Select(TodoItemDto.From).ToList()
        };
    }
}
