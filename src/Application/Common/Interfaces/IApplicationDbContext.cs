using TransfusionAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace TransfusionAPI.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade DatabaseFacade { get; }

    DbSet<Donor> Donors { get; }
    DbSet<Administrator> Administrators { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel);
}
