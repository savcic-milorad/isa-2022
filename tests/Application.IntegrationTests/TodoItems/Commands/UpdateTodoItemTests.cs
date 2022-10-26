using TransfusionAPI.Application.Common.Exceptions;
using TransfusionAPI.Application.TodoItems.Commands.CreateTodoItem;
using TransfusionAPI.Application.TodoItems.Commands.UpdateTodoItem;
using TransfusionAPI.Application.TodoLists.Commands.CreateTodoList;
using TransfusionAPI.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TransfusionAPI.Application.IntegrationTests.TodoItems.Commands;

[Collection("WebApplicationWithDatabaseCollectionFixture")]
public class UpdateTodoItemTests
{
    private readonly WebApplicationWithDatabaseFixture _fixture;

    public UpdateTodoItemTests(WebApplicationWithDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldRequireValidTodoItemId()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        var command = new UpdateTodoItemCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => _fixture.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ShouldUpdateTodoItem()
    {
        await _fixture.ResetState();
        var userId = await _fixture.RunAsDefaultUserAsync();

        var listId = await _fixture.SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await _fixture.SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new UpdateTodoItemCommand
        {
            Id = itemId,
            Title = "Updated Item Title"
        };

        await _fixture.SendAsync(command);

        var item = await _fixture.FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.Title.Should().Be(command.Title);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().NotBeNull();
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
