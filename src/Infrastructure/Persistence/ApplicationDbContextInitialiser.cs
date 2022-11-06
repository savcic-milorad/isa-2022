using TransfusionAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using TransfusionAPI.Application.Common.Constants;

namespace TransfusionAPI.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync(bool shouldDeleteDatabase = false)
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                if(shouldDeleteDatabase)
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

    public async Task TrySeedAsync(bool shouldSeedRoles = true, bool shouldSeedUsers = true)
    {
        var supportedRoles = new List<ApplicationRole>()
        {
            new ApplicationRole(SupportedRoles.Administrator),
            new ApplicationRole(SupportedRoles.Donor),
            new ApplicationRole(SupportedRoles.Staff)
        };

        foreach (var supportedRole in supportedRoles)
        {
            var applicationUsersForSupportedRole = new List<ApplicationUser>();
            if (_roleManager.Roles.All(r => r.Name != supportedRole.Name)
                && shouldSeedRoles)
            {
                _logger.LogInformation("Seeding database with Role: \n{@supportedRole}", supportedRole);
                await _roleManager.CreateAsync(supportedRole);
            }
            else
                continue;

            switch (supportedRole.Name)
            {
                case SupportedRoles.Administrator:
                    applicationUsersForSupportedRole.Add(new ApplicationUser("administrator@mail.com"));
                    break;
                case SupportedRoles.Donor:
                    applicationUsersForSupportedRole.Add(new ApplicationUser("donor@mail.com"));
                    break;
                case SupportedRoles.Staff:
                    applicationUsersForSupportedRole.Add(new ApplicationUser("staff@mail.com"));
                    break;
                default:
                    _logger.LogError("Unsupported {role} while seeding default users", supportedRole.NormalizedName);
                    throw new NotImplementedException();
            }

            foreach (var applicationUserForSupportedRole in applicationUsersForSupportedRole)
            {
                if (_userManager.Users.All(u => u.UserName != applicationUserForSupportedRole.UserName)
                    && shouldSeedUsers)
                {
                    await _userManager.CreateAsync(applicationUserForSupportedRole, "Password1!");
                    await _userManager.AddToRolesAsync(applicationUserForSupportedRole, new[] { supportedRole.Name });
                    _logger.LogInformation("{Role} role has following user: \n{@User}", supportedRole, applicationUserForSupportedRole);
                }
                else
                    continue;
            }
        }
    }
}
