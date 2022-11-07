namespace TransfusionAPI.Domain.Exceptions;

public class UnsupportedRoleException : Exception
{
    private UnsupportedRoleException(string message) : base(message)
    {
    }

    public static UnsupportedRoleException AssignmentOfUnsupportedRole(string role)
    {
        return new UnsupportedRoleException($"Assignment of unsupported role {role} attempted");
    }
}
