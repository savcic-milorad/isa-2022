using FakeItEasy;
using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Infrastructure.Identity;
using TransfusionAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using TransfusionAPI.Domain.Entities;
using TransfusionAPI.Domain.Constants;

namespace TransfusionAPI.Application.IntegrationTests;

public class WebApplicationWithDatabaseFixture
{
    private readonly ICurrentUserService _currentUserService;

    private readonly WebApplicationFactory<Program> _factory;
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Checkpoint _checkpoint;

    public WebApplicationWithDatabaseFixture()
    {
        _currentUserService = A.Fake<ICurrentUserService>();

        _factory = new CustomWebApplicationFactory(_currentUserService);
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();

        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { new Respawn.Graph.Table("__EFMigrationsHistory") }
        };
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public void RunAsUnknownUser()
    {
        A.CallTo(() => _currentUserService.UserId).Returns(null);
    }

    public async Task<string> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    }

    public async Task<string> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { "Administrator" });
    }

    public async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Infrastructure.Identity.ApplicationUser>>();

        var user = new Infrastructure.Identity.ApplicationUser(userName);

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new ApplicationRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            A.CallTo(() => _currentUserService.UserId).Returns(user.Id);
            return await Task.FromResult(user.Id);
        }

        var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    }

    public async Task ResetState()
    {
        using var scope = _scopeFactory.CreateScope();

        await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.TrySeedAsync(shouldSeedUsers: false);
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public async Task<(Infrastructure.Identity.ApplicationUser? applicationUser, Donor? donor)> FindDonorAsync(string username)
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Infrastructure.Identity.ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        
        var applicationUser = await userManager.FindByEmailAsync(username);
        if (applicationUser is null)
            throw new Exception($"Application user with {username} could not be found");

        var isInRole = await userManager.IsInRoleAsync(applicationUser, SupportedRoles.Donor);
        if(!isInRole)
            throw new Exception($"Application user with {username} is not in the donor role {SupportedRoles.Donor}");

        var donor = await context.Donors.SingleOrDefaultAsync(d => d.ApplicationUserId == applicationUser.Id);
        return (applicationUser, donor);
    }

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }
}
