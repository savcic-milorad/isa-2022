using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TransfusionAPI.Application.Common.Exceptions;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;
using Xunit;

namespace TransfusionAPI.Application.IntegrationTests.Identity.Commands;

[Collection("WebApplicationWithDatabaseCollectionFixture")]
public class CreateDonorTests
{
    private readonly WebApplicationWithDatabaseFixture _fixture;

    public CreateDonorTests(WebApplicationWithDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateDonorCommandHandler_WhenCommandInvalid_ThenValidationExceptionThrown()
    {
        // Arrange
        await _fixture.ResetState();
        _fixture.RunAsUnknownUser();

        // Act
        var command = new CreateDonorCommand();

        // Assert
        await FluentActions.Invoking(() =>
            _fixture.SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateDonorCommandHandler_WhenCommandValid_ThenDonorCreatedAndReferencesApplicationUser()
    {
        // Arrange
        await _fixture.ResetState();
        var userId = await _fixture.RunAsDefaultUserAsync();

        var createDonorCommand = new CreateDonorCommand
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
        };
        
        // Act
        var createDonorResult = await _fixture.SendAsync(createDonorCommand);

        // Assert
        createDonorResult.Succeeded.Should().BeTrue();

        var (applicationUser, donor) = await _fixture.FindDonorAsync(createDonorCommand.UserName);

        applicationUser.Should().NotBeNull();

        donor.Should().NotBeNull();
        donor!.FirstName.Should().Be(createDonorCommand.FirstName);
        donor!.LastName.Should().Be(createDonorCommand.LastName);
        donor!.Sex.Should().Be(createDonorCommand.Sex);
        donor!.JMBG.Should().Be(createDonorCommand.JMBG);
        donor!.State.Should().Be(createDonorCommand.State);
        donor!.HomeAddress.Should().Be(createDonorCommand.HomeAddress);
        donor!.City.Should().Be(createDonorCommand.City);
        donor!.PhoneNumber.Should().Be(createDonorCommand.PhoneNumber);
        donor!.Occupation.Should().Be(createDonorCommand.Occupation);
        donor!.OccupationInfo.Should().Be(createDonorCommand.OccupationInfo);
        donor!.CreatedBy.Should().Be(userId);
        donor!.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        donor!.LastModifiedBy.Should().Be(userId);
        donor!.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
