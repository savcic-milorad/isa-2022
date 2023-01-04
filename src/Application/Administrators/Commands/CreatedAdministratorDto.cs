using TransfusionAPI.Domain.Entities;

namespace TransfusionAPI.Application.Donors.Commands;

public class CreatedAdministratorDto
{
    public int Id { get; private set; }
    public string ApplicationUserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }

    private CreatedAdministratorDto(int id, string applicationUserId, string firstName, string lastName, string phoneNumber)
    {
        Id = id;
        ApplicationUserId = applicationUserId;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    public static CreatedAdministratorDto From(Administrator administrator)
    {
        return new CreatedAdministratorDto(administrator.Id, administrator.ApplicationUserId, administrator.FirstName, administrator.LastName, administrator.PhoneNumber);
    }
}