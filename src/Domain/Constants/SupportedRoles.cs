namespace TransfusionAPI.Domain.Constants;

public static class SupportedRoles
{
    public const string Donor = "Donor";
    public const string Staff = "Staff";
    public const string Administrator = "Administrator";

    public readonly static List<string> All = new List<string>()
    {
        Donor,
        Staff,
        Administrator
    };
}
