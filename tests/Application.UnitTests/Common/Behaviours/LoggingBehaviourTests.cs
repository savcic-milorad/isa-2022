using FakeItEasy;
using TransfusionAPI.Application.Common.Behaviours;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.Extensions.Logging;
using Xunit;

namespace TransfusionAPI.Application.UnitTests.Common.Behaviours;

public class LoggingBehaviourTests
{
    private readonly ILogger<CreateTodoItemCommand> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly LoggingBehaviour<CreateTodoItemCommand> _sut;

    public LoggingBehaviourTests()
    {
        _logger = A.Fake<ILogger<CreateTodoItemCommand>>();
        _currentUserService = A.Fake<ICurrentUserService>();
        _identityService = A.Fake<IIdentityService>();

        _sut = new LoggingBehaviour<CreateTodoItemCommand>(_logger, _currentUserService, _identityService);
    }

    [Fact]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        // Arrange
        A.CallTo(() => _currentUserService.UserId).Returns(Guid.NewGuid().ToString());
        var callToIdentityService = A.CallTo(() => _identityService.GetUserNameAsync(A<string>.Ignored));

        // Act
        await _sut.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        // Assert
        callToIdentityService.MustHaveHappenedOnceExactly();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated(string userIdValue)
    {
        // Arrange
        var callToCurrentUserService = A.CallTo(() => _currentUserService.UserId);
        callToCurrentUserService.Returns(userIdValue);

        var callToIdentityService = A.CallTo(() => _identityService.GetUserNameAsync(A<string>.Ignored));

        // Act
        await _sut.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        // Assert
        callToIdentityService.MustNotHaveHappened();
        callToCurrentUserService.MustHaveHappenedOnceExactly();
    }
}
