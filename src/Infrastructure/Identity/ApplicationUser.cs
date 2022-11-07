using Microsoft.AspNetCore.Identity;

namespace TransfusionAPI.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser() : base()
    {
    }

    public ApplicationUser(string userName) : base(userName)
    {
        Email = userName;
    }
}
