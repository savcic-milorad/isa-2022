using MediatR;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;
using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.Identity.Queries.GetApplicationUser;

public record GetApplicationUserQuery : IRequest<Result<ApplicationUser>>
{
    public string ApplicationUserId { get; set; } = string.Empty;
}

public class GetApplicationUserQueryHandler : IRequestHandler<GetApplicationUserQuery, Result<ApplicationUser>>
{
    private readonly IIdentityService _identityService;

    public GetApplicationUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<ApplicationUser>> Handle(GetApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var applicationUserResult = await _identityService.GetUserByUsedIdAsync(request.ApplicationUserId);
        return applicationUserResult;
    }
}
