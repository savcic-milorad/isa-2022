using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransfusionAPI.Domain.Events;

public class DonorCreatedEvent : BaseEvent
{
    public DonorCreatedEvent(Donor donor)
    {
        Donor = donor;
    }

    public Donor Donor { get; }
}
