using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.Identity.Queries.GetApplicationUser;

public class ApplicationUserDto
{
    public string UserName { get; private set; }
    public string AssignedRole { get; private set; }
    public bool IsConfirmed { get; private set; }

    private ApplicationUserDto(string userName, string assignedRole, bool isConfirmed)
    {
        UserName = userName;
        AssignedRole = assignedRole;
        IsConfirmed = isConfirmed;
    }

    public static ApplicationUserDto From(ApplicationUser applicationUser)
    {
        return new ApplicationUserDto(applicationUser.UserName, applicationUser.AssignedRole, applicationUser.IsConfirmed);
    }
}
