using TransfusionAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using TransfusionAPI.Domain.Constants;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Identity.Commands.CreateDonor;
using TransfusionAPI.Application.Donors.Commands;

namespace TransfusionAPI.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUserIdentity> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IIdentityService _identityService;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUserIdentity> userManager, RoleManager<ApplicationRole> roleManager, IIdentityService identityService)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _identityService = identityService;
    }

    public async Task InitialiseAsync(bool shouldDeleteDatabase = false)
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                if (shouldDeleteDatabase)
                {
                    _logger.LogInformation("Ensuring database is deleted.");
                    await _context.Database.EnsureDeletedAsync();
                }

                _logger.LogInformation("Listing already applied migrations to the database, should be empty if database was deleted.");
                var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync();
                var appliedMigrationsString = appliedMigrations.Aggregate(
                    new StringBuilder("\n\n"),
                    (acc, val) => acc.Append($"{val}\n"),
                    acc => acc.ToString());

                _logger.LogInformation("Executing all pending migrations on the database.");
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                var pendingMigrationsString = pendingMigrations.Aggregate(
                    new StringBuilder("\n\n"),
                    (acc, val) => acc.Append($"{val}\n"),
                    acc => acc.ToString());

                _logger.LogInformation("Applied migrations: {appliedMigrations}", appliedMigrationsString);
                _logger.LogInformation("Pending migrations: {pendingMigrations}", pendingMigrationsString);

                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var supportedRoles = new List<ApplicationRole>()
        {
            new ApplicationRole(SupportedRoles.Administrator),
            new ApplicationRole(SupportedRoles.Donor),
            new ApplicationRole(SupportedRoles.Staff)
        };

        var donors = new List<ApplicationUserIdentity>();
        var administrators = new List<ApplicationUserIdentity>();
        var staff = new List<ApplicationUserIdentity>();


        foreach (var supportedRole in supportedRoles)
        {
            if (_roleManager.Roles.Any(r => r.Name == supportedRole.Name))
                continue;
            
            _logger.LogInformation("Seeding database with Role: \n{@supportedRole}", supportedRole);
            await _roleManager.CreateAsync(supportedRole);
        }

        staff.Add(new ApplicationUserIdentity("staff@mail.com"));
        foreach (var staffMember in staff)
        {
            if (_userManager.Users.Any(u => u.UserName == staffMember.UserName))
                continue;

            await _userManager.CreateAsync(staffMember, "Password1!");
            await _userManager.AddToRolesAsync(staffMember, new[] { SupportedRoles.Staff });
            _logger.LogInformation("{Role} role has following user: \n{@User}", SupportedRoles.Staff, staffMember);
        }

        donors.Add(new ApplicationUserIdentity("donor@mail.com"));
        foreach (var donor in donors)
        {
            if (_userManager.Users.Any(u => u.UserName == donor.UserName))
                continue;

            var donorCreateCommandHandler = new CreateDonorCommandHandler(_context, _identityService);

            var createdDonor = await donorCreateCommandHandler.Handle(new CreateDonorCommand()
            {
                FirstName = "Seed",
                LastName = "Seeder",
                Sex = Domain.Enums.Sex.Male,
                JMBG = "1122333445566",
                State = SupportedStates.SupportedStatesArray.First(),
                HomeAddress = "Address",
                City = "Novi Sad",
                PhoneNumber = "0211234567",
                Occupation = "Seed data",
                OccupationInfo = "Seeding data",
                UserName = donor.UserName,
                Password = "Password1!"
            }, CancellationToken.None);

            _logger.LogInformation("\nCreated donor: {@Donor}", createdDonor.Payload);
        }

        administrators.Add(new ApplicationUserIdentity("administrator@mail.com"));
        foreach (var administrator in administrators)
        {
            if (_userManager.Users.Any(u => u.UserName == administrator.UserName))
                continue;

            var createAdministratorCommandHandler = new CreateAdministratorCommandHandler(_context, _identityService);

            var createdAdministrator = await createAdministratorCommandHandler.Handle(new CreateAdministratorCommand()
            {
                UserName = administrator.UserName,
                Password = "Password1!",
                FirstName = "Seed",
                LastName = "Seeder",
                PhoneNumber = "0211234567"
            }, CancellationToken.None);

            _logger.LogInformation("\nCreated aadministrator: {@Administrator}", createdAdministrator.Payload);
        }
    }
}
