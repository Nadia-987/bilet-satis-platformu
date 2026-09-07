
using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _context.Events
            .Select(e => new
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
            })
            .ToListAsync();

        return Ok(events);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(Event newEvent)
    {
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        return Ok(newEvent);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventItem = await _context.Events.FindAsync(id);

        if (eventItem == null)
        {
            return NotFound();
        }

        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

