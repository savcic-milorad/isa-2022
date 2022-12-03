using TransfusionAPI.Application.Common.Models;
using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> AuthenticateByCredentials(string username, string password);
    Task<Result<ApplicationUser>> GetUserByUsedIdAsync(string userId);
    Task<string> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<Result<string>> CreateUserAsync(string userName, string password);
    Task<Result<string>> CreateDonorAsync(string userName, string password);
    Task<Result> DeleteUserAsync(string userId);
}
