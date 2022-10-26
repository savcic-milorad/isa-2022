using TransfusionAPI.Application.Common.Exceptions;
using TransfusionAPI.Application.TodoLists.Commands.CreateTodoList;
using TransfusionAPI.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace TransfusionAPI.Application.IntegrationTests.TodoLists.Commands;

[Collection("WebApplicationWithDatabaseCollectionFixture")]
public class CreateTodoListTests
{
    private readonly WebApplicationWithDatabaseFixture _fixture;

    public CreateTodoListTests(WebApplicationWithDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldRequireMinimumFields()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        var command = new CreateTodoListCommand();
        await FluentActions.Invoking(() => _fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldRequireUniqueTitle()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        await _fixture.SendAsync(new CreateTodoListCommand
        {
            Title = "Shopping"
        });

        var command = new CreateTodoListCommand
        {
            Title = "Shopping"
        };

        await FluentActions.Invoking(() =>
            _fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ShouldCreateTodoList()
    {
        await _fixture.ResetState();
        var userId = await _fixture.RunAsDefaultUserAsync();

        var command = new CreateTodoListCommand
        {
            Title = "Tasks"
        };

        var id = await _fixture.SendAsync(command);

        var list = await _fixture.FindAsync<TodoList>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
