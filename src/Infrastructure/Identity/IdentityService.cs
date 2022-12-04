using TransfusionAPI.Application.Common.Interfaces;
using TransfusionAPI.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransfusionAPI.Domain.Constants;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace TransfusionAPI.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUserIdentity> _userManager;
    private readonly SignInManager<ApplicationUserIdentity> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUserIdentity> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public IdentityService(
        SignInManager<ApplicationUserIdentity> signInManager,
        UserManager<ApplicationUserIdentity> userManager,
        IUserClaimsPrincipalFactory<ApplicationUserIdentity> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService, RoleManager<ApplicationRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _roleManager = roleManager;
    }

    public async Task<Result<string>> AuthenticateByCredentials(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if(user is null)
            return Result.Failure< string>(default, $"User with credentials, username: {username}, does not exist");

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!signInResult.Succeeded)
            return Result.Failure<string>(default, $"User with credentials, username: {username} and pw: {password}, could not sign in");

        var associatedRoles = await _userManager.GetRolesAsync(user);
        var associatedRole = associatedRoles.SingleOrDefault();

        if(associatedRole is null)
            return Result.Failure<string>(default, $"User with credentials, username: {username} and pw: {password}, does not have an assigned role");

        var claims = new List<Claim>()
        {
            new Claim("role", associatedRole),
            new Claim("username", user.NormalizedUserName),
            new Claim("userId", user.Id)
        };

        var secretKeyBytes = Encoding.ASCII.GetBytes("token-generation-secret");
        var jwt = new JwtSecurityToken(
            issuer: "transfusion-api",
            audience: "transfusion-api",
            claims: claims, 
            notBefore: DateTime.UtcNow, 
            expires: DateTime.UtcNow.AddYears(2), 
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return Result.Success<string>(tokenHandler.WriteToken(jwt));
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

        var domainApplicationUser = new Domain.Entities.ApplicationUser(user.NormalizedUserName, roleAssignedToUser, user.EmailConfirmed);
        return Result.Success(domainApplicationUser);
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<Result<string>> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUserIdentity(userName);

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

    public async Task<Result> DeleteUserAsync(ApplicationUserIdentity user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result<string>> CreateDonorAsync(string userName, string password)
    {
        var user = new ApplicationUserIdentity(userName);

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return result.ToApplicationResult<string>();

        var addToRoleResult = await _userManager.AddToRoleAsync(user, SupportedRoles.Donor);

        return addToRoleResult.ToApplicationResult(user.Id);
    }
}
