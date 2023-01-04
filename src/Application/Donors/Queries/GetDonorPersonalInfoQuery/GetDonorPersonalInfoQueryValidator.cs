using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Domain.Constants;

namespace TransfusionAPI.Application.Donors.Queries.GetDonorPersonalInfoQuery;

public class GetDonorPersonalInfoQueryValidator : AbstractValidator<GetDonorPersonalInfoQuery>
{
    public GetDonorPersonalInfoQueryValidator(ICurrentUserService currentUserService, IIdentityService identityService, IApplicationDbContext context)
    {
        RuleFor(v => v.DonorId)
            .NotEmpty()
            .MustAsync(async (donorId, ct) =>
            {
                var currentUserIsInAdministratorRole = await identityService.IsInRoleAsync(currentUserService.UserId ?? string.Empty, SupportedRoles.Administrator);
                if (currentUserIsInAdministratorRole)
                    return true;

                var donor = await context.Donors.SingleOrDefaultAsync(d => d.Id == donorId);
                if (donor is null)
                    return false;

                var foundDonorIsCurrentUser = donor.ApplicationUserId == currentUserService.UserId;
                return foundDonorIsCurrentUser;
            }).WithMessage("With your permissions you are only able to access your own profile.");
    }
}
