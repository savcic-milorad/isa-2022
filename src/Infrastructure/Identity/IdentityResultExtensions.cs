using TransfusionAPI.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace TransfusionAPI.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }

    public static Result<T> ToApplicationResult<T>(this IdentityResult result, T? payload = default)
    {
        return result.Succeeded
            ? Result.Success(payload!)
            : Result.Failure(payload, result.Errors.Select(e => e.Description));
    }
}
