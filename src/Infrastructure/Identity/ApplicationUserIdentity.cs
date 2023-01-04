using Microsoft.AspNetCore.Identity;

namespace TransfusionAPI.Infrastructure.Identity;

public class ApplicationUserIdentity : IdentityUser
{
    public ApplicationUserIdentity() : base()
    {
    }

    public ApplicationUserIdentity(string userName) : base(userName)
    {
        Email = userName;
    }
}
