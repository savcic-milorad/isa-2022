using TransfusionAPI.Domain.Constants;
using TransfusionAPI.Domain.Exceptions;

namespace TransfusionAPI.Domain.Entities;

public class ApplicationUser
{
    public string UserName { get; private set; }
    public string AssignedRole { get; private set; }
    public bool IsConfirmed { get; private set; }

    public ApplicationUser(string userName, string assignedRole, bool isConfirmed = true)
    {
        if(!SupportedRoles.All.Contains(assignedRole))
            throw UnsupportedRoleException.AssignmentOfUnsupportedRole(assignedRole);

        UserName = userName;
        IsConfirmed = isConfirmed;
        AssignedRole = assignedRole;
    }
}
