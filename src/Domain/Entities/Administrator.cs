namespace TransfusionAPI.Domain.Entities;

public class Administrator : BaseAuditableEntity
{
    public string ApplicationUserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }

    public Administrator(string ApplicationUserId, string FirstName, string LastName, string PhoneNumber)
    {
        this.ApplicationUserId = ApplicationUserId;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.PhoneNumber = PhoneNumber;
    }
}
