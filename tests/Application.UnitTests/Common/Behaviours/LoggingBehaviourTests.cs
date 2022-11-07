using FakeItEasy;
using TransfusionAPI.Application.Common.Behaviours;
using TransfusionAPI.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Xunit;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;

namespace TransfusionAPI.Application.UnitTests.Common.Behaviours;

public class LoggingBehaviourTests
{
    private readonly ILogger<CreateDonorCommand> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly LoggingBehaviour<CreateDonorCommand> _sut;

    public LoggingBehaviourTests()
    {
        _logger = A.Fake<ILogger<CreateDonorCommand>>();
        _currentUserService = A.Fake<ICurrentUserService>();
        _identityService = A.Fake<IIdentityService>();

        _sut = new LoggingBehaviour<CreateDonorCommand>(_logger, _currentUserService, _identityService);
    }

    [Fact]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        // Arrange
        A.CallTo(() => _currentUserService.UserId).Returns(Guid.NewGuid().ToString());
        var callToIdentityService = A.CallTo(() => _identityService.GetUserNameAsync(A<string>.Ignored));

        // Act
        await _sut.Process(
            new CreateDonorCommand
            {
                UserName = "johnsmith@mail.org",
                Password = "John1234",
                FirstName = "John",
                LastName = "Smith",
                Sex = Domain.Enums.Sex.Male,
                JMBG = "0101001001122",
                State = "Serbia",
                HomeAddress = "Narodnog Fronta",
                City = "Novi Sad",
                PhoneNumber = "064123456",
                Occupation = "QA",
                OccupationInfo = "Testing code"
            }, new CancellationToken());

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
        await _sut.Process(new CreateDonorCommand
        {
            UserName = "johnsmith@mail.org",
            Password = "John1234",
            FirstName = "John",
            LastName = "Smith",
            Sex = Domain.Enums.Sex.Male,
            JMBG = "0101001001122",
            State = "Serbia",
            HomeAddress = "Narodnog Fronta",
            City = "Novi Sad",
            PhoneNumber = "064123456",
            Occupation = "QA",
            OccupationInfo = "Testing code"
        }, new CancellationToken());

        // Assert
        callToIdentityService.MustNotHaveHappened();
        callToCurrentUserService.MustHaveHappenedOnceExactly();
    }
}
