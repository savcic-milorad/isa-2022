using TransfusionAPI.Domain.Entities;
using TransfusionAPI.Domain.Enums;

namespace TransfusionAPI.Application.Identity.Commands.CreateDonor;

public class CreatedDonorDto
{
    public int Id { get; private set; }
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

    private CreatedDonorDto(int id, string applicationUserId, string firstName, string lastName, Sex sex, string jMBG, string state, string homeAddress, string city, string phoneNumber, string occupation, string occupationInfo)
    {
        Id = id;
        ApplicationUserId = applicationUserId;
        FirstName = firstName;
        LastName = lastName;
        Sex = sex;
        JMBG = jMBG;
        State = state;
        HomeAddress = homeAddress;
        City = city;
        PhoneNumber = phoneNumber;
        Occupation = occupation;
        OccupationInfo = occupationInfo;
    }

    public static CreatedDonorDto From(Donor donor)
    {
        return new CreatedDonorDto(donor.Id, donor.ApplicationUserId, donor.FirstName, donor.LastName, donor.Sex, donor.JMBG, donor.State, donor.HomeAddress, donor.City, donor.PhoneNumber, donor.Occupation, donor.OccupationInfo);
    }

}
