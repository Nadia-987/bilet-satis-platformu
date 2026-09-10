using EventTicket.Domain.Entities;

namespace EventTicket.API.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<Event>> GetEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
    Task<Event> CreateEventAsync(Event newEvent);
    Task<bool> DeleteEventAsync(int id);
}
