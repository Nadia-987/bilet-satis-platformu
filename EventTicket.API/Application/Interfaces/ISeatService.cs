using EventTicket.Domain.Entities;

namespace EventTicket.API.Application.Interfaces;

public interface ISeatService
{
    Task<IEnumerable<Seat>> GetSeatsAsync();
    Task<Seat> CreateSeatAsync(Seat seat);
}
