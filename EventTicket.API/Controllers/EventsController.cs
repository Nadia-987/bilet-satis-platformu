
using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _eventService.GetEventsAsync();

        var result = events.Select(e => new
        {
            e.Id,
            e.Name,
            e.Description,
            e.StartDate,
            e.VenueId,
            Venue = e.Venue == null ? null : new
            {
                e.Venue.Id,
                e.Venue.Name,
                Sections = e.Venue.Sections.Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Type,
                    s.Capacity,
                    s.Price,
                    Seats = s.Seats.Select(seat => new
                    {
                        seat.Id,
                        seat.Row,
                        seat.Number
                    }).ToList()
                }).ToList()
            }
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(Event newEvent)
    {
        var createdEvent = await _eventService.CreateEventAsync(newEvent);

        return Ok(createdEvent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var deleted = await _eventService.DeleteEventAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

       return NoContent();
    }
}


