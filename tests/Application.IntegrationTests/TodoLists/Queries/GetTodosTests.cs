using TransfusionAPI.Application.TodoLists.Queries.GetTodos;
using TransfusionAPI.Domain.Entities;
using TransfusionAPI.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace TransfusionAPI.Application.IntegrationTests.TodoLists.Queries;

[Collection("WebApplicationWithDatabaseCollectionFixture")]
public class GetTodosTests
{
    private readonly WebApplicationWithDatabaseFixture _fixture;

    public GetTodosTests(WebApplicationWithDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldReturnPriorityLevels()
    {
        await _fixture.ResetState();
        await _fixture.RunAsDefaultUserAsync();

        var query = new GetTodosQuery();

        var result = await _fixture.SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ShouldReturnAllListsAndItems()
    {
        await _fixture.ResetState();
        await _fixture.RunAsDefaultUserAsync();

        await _fixture.AddAsync(new TodoList
        {
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" }
                    }
        });

        var query = new GetTodosQuery();

        var result = await _fixture.SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(7);
    }

    [Fact]
    public async Task ShouldDenyAnonymousUser()
    {
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        var query = new GetTodosQuery();

        var action = () => _fixture.SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
