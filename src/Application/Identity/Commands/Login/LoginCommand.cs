using MediatR;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;

namespace TransfusionAPI.Application.Identity.Commands.Login;

public class LoginCommand : IRequest<Result<LoginSuccessDto>>
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public class CreateDonorCommandHandler : IRequestHandler<LoginCommand, Result<LoginSuccessDto>>
{
    private readonly IIdentityService _identityService;

    public CreateDonorCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<LoginSuccessDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var authenticateResult = await _identityService.AuthenticateByCredentials(command.UserName, command.Password);
        if (!authenticateResult.Succeeded)
            return Result.Failure<LoginSuccessDto>(default, authenticateResult.Errors);

        var jwt = authenticateResult.Payload.ToString();
        return Result.Success(LoginSuccessDto.From(jwt));
    }
}
