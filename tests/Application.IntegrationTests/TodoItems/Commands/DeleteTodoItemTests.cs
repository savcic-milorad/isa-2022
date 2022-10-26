using TransfusionAPI.Application.Common.Exceptions;
using TransfusionAPI.Application.TodoItems.Commands.CreateTodoItem;
using TransfusionAPI.Application.TodoItems.Commands.DeleteTodoItem;
using TransfusionAPI.Application.TodoLists.Commands.CreateTodoList;
using TransfusionAPI.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TransfusionAPI.Application.IntegrationTests.TodoItems.Commands;

[Collection("WebApplicationWithDatabaseCollectionFixture")]
public class DeleteTodoItemTests
{
    private readonly WebApplicationWithDatabaseFixture _fixture;

    public DeleteTodoItemTests(WebApplicationWithDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldRequireValidTodoItemId()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        var command = new DeleteTodoItemCommand(99);

        await FluentActions.Invoking(() =>
            _fixture.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ShouldDeleteTodoItem()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        var listId = await _fixture.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await _fixture.SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await _fixture.SendAsync(new DeleteTodoItemCommand(itemId));

        var item = await _fixture.FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
