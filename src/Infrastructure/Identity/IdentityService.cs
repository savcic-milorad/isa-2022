using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransfusionAPI.Domain.Constants;

namespace TransfusionAPI.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<Result<Domain.Entities.ApplicationUser>> GetUserByUsedIdAsync(string userId)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
        if (user is null)
            return Result.Failure<Domain.Entities.ApplicationUser>(default, $"User with id <{userId}> could not be found");

        var roles = await _userManager.GetRolesAsync(user);
        var roleAssignedToUser = roles.SingleOrDefault();
        if(roleAssignedToUser is null)
            return Result.Failure<Domain.Entities.ApplicationUser>(default, $"User with id <{userId}> does not have a role assigned");

        var domainApplicationUser = new Domain.Entities.ApplicationUser(user.NormalizedUserName, roleAssignedToUser);
        return Result.Success(domainApplicationUser);
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<Result<string>> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser(userName);

        var result = await _userManager.CreateAsync(user, password);

        return result.ToApplicationResult(user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result<string>> CreateDonorAsync(string userName, string password)
    {
        var user = new ApplicationUser(userName);

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return result.ToApplicationResult<string>();

        var addToRoleResult = await _userManager.AddToRoleAsync(user, SupportedRoles.Donor);

        return addToRoleResult.ToApplicationResult(user.Id);
    }
}
