using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Repositories;

namespace EventTicket.API.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<Event>> GetEventsAsync()
    {
        return await _eventRepository.GetAllAsync();
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task<Event> CreateEventAsync(Event newEvent)
    {
        await _eventRepository.AddAsync(newEvent);
        await _eventRepository.SaveAsync();

        return newEvent;
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var eventItem = await _eventRepository.GetByIdAsync(id);

        if (eventItem == null)
            return false;

        await _eventRepository.DeleteAsync(eventItem);
        await _eventRepository.SaveAsync();

        return true;
    }
}