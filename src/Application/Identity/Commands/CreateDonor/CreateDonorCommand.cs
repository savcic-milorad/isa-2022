using MediatR;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;
using TransfusionAPI.Domain.Entities;
using TransfusionAPI.Domain.Enums;
using TransfusionAPI.Domain.Events;

namespace TransfusionAPI.Application.Identity.Commands.CreateDonor;

public record CreateDonorCommand : IRequest<Result<string>>
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public Sex Sex { get; init; }
    public string JMBG { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string HomeAddress { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Occupation { get; init; } = string.Empty;
    public string OccupationInfo { get; init; } = string.Empty;
}

public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, Result<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public CreateDonorCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<Result<string>> Handle(CreateDonorCommand command, CancellationToken cancellationToken)
    {
        using (var transaction = await _context.DatabaseFacade.BeginTransactionAsync())
        {
            var createDonorResult = await _identityService.CreateDonorAsync(command.UserName, command.Password);
            if (!createDonorResult.Succeeded)
            {
                await transaction.RollbackAsync();
                return createDonorResult;
            }

            var entity = new Donor(
                ApplicationUserId: createDonorResult.Payload,
                command.FirstName,
                command.LastName,
                command.Sex,
                command.JMBG,
                command.State,
                command.HomeAddress,
                command.City,
                command.PhoneNumber,
                command.Occupation,
                command.OccupationInfo);

            entity.AddDomainEvent(new DonorCreatedEvent(entity));

            _context.Donors.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync();

            return Result.Success(entity.ApplicationUserId);
        }
    }
}
