using FluentValidation;

namespace TransfusionAPI.Application.Identity.Queries.GetApplicationUser;

public class GetApplicationUserQueryValidator : AbstractValidator<GetApplicationUserQuery>
{
    public GetApplicationUserQueryValidator()
    {
        RuleFor(v => v.ApplicationUserId).NotEmpty();
    }
}
