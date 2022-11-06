namespace TransfusionAPI.Domain.Entities;

public class Donor : BaseAuditableEntity
{
    public string ApplicationUserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Sex Sex { get; private set; }
    public string JMBG { get; private set; }
    public string State { get; private set; }
    public string HomeAddress { get; private set; }
    public string City { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Occupation { get; private set; }
    public string OccupationInfo { get; private set; }

    public Donor(string ApplicationUserId, string FirstName, string LastName, Sex Sex, string JMBG, string State, string HomeAddress, string City, string PhoneNumber, string Occupation, string OccupationInfo)
    {
        this.ApplicationUserId = ApplicationUserId;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Sex = Sex;
        this.JMBG = JMBG;
        this.State = State;
        this.HomeAddress = HomeAddress;
        this.City = City;
        this.PhoneNumber = PhoneNumber;
        this.Occupation = Occupation;
        this.OccupationInfo = OccupationInfo;
    }
}
