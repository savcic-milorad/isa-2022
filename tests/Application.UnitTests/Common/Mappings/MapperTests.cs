using System.Runtime.Serialization;
using AutoMapper;
using TransfusionAPI.Application.Common.Mappings;
using TransfusionAPI.Application.Common.Models;
using TransfusionAPI.Application.TodoLists.Queries.GetTodos;
using TransfusionAPI.Domain.Entities;
using Xunit;

namespace TransfusionAPI.Application.UnitTests.Common.Mappings;

public class MapperTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MapperTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());

        _mapper = _configuration.CreateMapper();
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Theory]
    [InlineData(typeof(TodoList), typeof(TodoListDto))]
    [InlineData(typeof(TodoItem), typeof(TodoItemDto))]
    [InlineData(typeof(TodoList), typeof(LookupDto))]
    [InlineData(typeof(TodoItem), typeof(LookupDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        // Arrange
        var instance = GetInstanceOf(source);

        // Act
        // Assert
        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}
