using MediatR;
using Microsoft.EntityFrameworkCore;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;

namespace TransfusionAPI.Application.Donors.Queries.GetDonorPersonalInfoQuery;

public record GetDonorPersonalInfoQuery : IRequest<Result<DonorPersonalInfoDto>>
{
    public int DonorId { get; init; }
}

public class GetDonorPersonalInfoQueryHandler : IRequestHandler<GetDonorPersonalInfoQuery, Result<DonorPersonalInfoDto>>
{
    private readonly IApplicationDbContext _context;

    public GetDonorPersonalInfoQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<DonorPersonalInfoDto>> Handle(GetDonorPersonalInfoQuery request, CancellationToken cancellationToken)
    {
        var donor = await _context.Donors.SingleOrDefaultAsync(d => d.Id == request.DonorId);
        if (donor is null)
            return Result.Failure<DonorPersonalInfoDto>(default, $"Donor with Id {request.DonorId} could not be found");

        var donorPersonalInfoDto = DonorPersonalInfoDto.From(donor);

        return Result.Success(donorPersonalInfoDto);
    }
}
