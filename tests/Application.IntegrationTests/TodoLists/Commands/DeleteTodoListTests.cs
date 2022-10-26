using TransfusionAPI.Application.Common.Exceptions;
using TransfusionAPI.Application.TodoLists.Commands.CreateTodoList;
using TransfusionAPI.Application.TodoLists.Commands.DeleteTodoList;
using TransfusionAPI.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TransfusionAPI.Application.IntegrationTests.TodoLists.Commands;

[Collection("WebApplicationWithDatabaseCollectionFixture")]
public class DeleteTodoListTests
{
    private readonly WebApplicationWithDatabaseFixture _fixture;

    public DeleteTodoListTests(WebApplicationWithDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldRequireValidTodoListId()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => _fixture.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ShouldDeleteTodoList()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        var listId = await _fixture.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await _fixture.SendAsync(new DeleteTodoListCommand(listId));

        var list = await _fixture.FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
