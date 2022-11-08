using MediatR;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;

namespace TransfusionAPI.Application.Identity.Queries.GetApplicationUser;

public record GetApplicationUserQuery : IRequest<Result<ApplicationUserDto>>
{
    public string ApplicationUserId { get; set; } = string.Empty;
}

public class GetApplicationUserQueryHandler : IRequestHandler<GetApplicationUserQuery, Result<ApplicationUserDto>>
{
    private readonly IIdentityService _identityService;

    public GetApplicationUserQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<ApplicationUserDto>> Handle(GetApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var applicationUserResult = await _identityService.GetUserByUsedIdAsync(request.ApplicationUserId);
        if (!applicationUserResult.Succeeded)
            return Result.Failure<ApplicationUserDto>(default, applicationUserResult.Errors);

        var applicationUserDto = ApplicationUserDto.From(applicationUserResult.Payload);
        return Result.Success(applicationUserDto);
    }
}
