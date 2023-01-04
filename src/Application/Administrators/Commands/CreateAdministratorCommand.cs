using MediatR;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;
using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.Donors.Commands;

public record CreateAdministratorCommand : IRequest<Result<CreatedAdministratorDto>>
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}

public class CreateAdministratorCommandHandler : IRequestHandler<CreateAdministratorCommand, Result<CreatedAdministratorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public CreateAdministratorCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<Result<CreatedAdministratorDto>> Handle(CreateAdministratorCommand command, CancellationToken cancellationToken)
    {
        using (var transaction = await _context.DatabaseFacade.BeginTransactionAsync())
        {
            var createAdministratorResult = await _identityService.CreateAdministratorAsync(command.UserName, command.Password);
            if (!createAdministratorResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return Result.Failure<CreatedAdministratorDto>(default, createAdministratorResult.Errors);
            }

            var entity = new Administrator(
                ApplicationUserId: createAdministratorResult.Payload,
                command.FirstName,
                command.LastName,
                command.PhoneNumber);

            _context.Administrators.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync();

            var administratorDto = CreatedAdministratorDto.From(entity);
            return Result.Success(administratorDto);
        }
    }
}
