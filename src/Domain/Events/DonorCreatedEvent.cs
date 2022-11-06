namespace TransfusionAPI.Domain.Events;

public class DonorCreatedEvent : BaseEvent
{
    public DonorCreatedEvent(Donor donor)
    {
        Donor = donor;
    }

    public Donor Donor { get; }
}
