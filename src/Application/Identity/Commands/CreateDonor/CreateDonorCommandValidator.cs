using FluentValidation;
using TransfusionAPI.Domain.Constants;

namespace TransfusionAPI.Application.Identity.Commands.CreateDonor;

public class CreateDonorCommandValidator : AbstractValidator<CreateDonorCommand>
{
    public CreateDonorCommandValidator()
    {
        RuleFor(v => v.UserName)
            .EmailAddress()
            .NotEmpty();
        RuleFor(v => v.Password)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
            .WithMessage("Password must contain minimum eight characters, at least one letter and one number");
        RuleFor(v => v.FirstName)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.LastName)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v => v.JMBG)
            .Length(13)
            .Matches(@"^[0-9]{13,13}$");
        RuleFor(v => v.State)
            .MaximumLength(50)
            .Must(SupportedStates.SupportedStatesArray.Contains)
            .WithMessage($"State must be one of the following states: {string.Join(", ", SupportedStates.SupportedStatesArray)}");
        RuleFor(v => v.HomeAddress)
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(v => v.City)
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(v => v.PhoneNumber)
            .Matches(@"^[0-9]{6,15}$")
            .WithMessage("Phone number must be between 6 and 15 digits.");
        RuleFor(v => v.Occupation)
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(v => v.OccupationInfo)
            .MaximumLength(500)
            .NotEmpty();
    }
}