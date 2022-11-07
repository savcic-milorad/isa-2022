using TransfusionAPI.Application.Common.Interfaces;

namespace TransfusionAPI.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
