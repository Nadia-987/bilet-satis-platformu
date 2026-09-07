using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VenuesController : ControllerBase
{
    private readonly AppDbContext _context;

    public VenuesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetVenues()
    {
        var venues = await _context.Venues
            .Select(v => new
            {
                v.Id,
                v.Name,
                Sections = v.Sections.Select(s => new
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
            })
            .ToListAsync();

        return Ok(venues);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVenue(Venue venue)
    {
        _context.Venues.Add(venue);
        await _context.SaveChangesAsync();

        return Ok(venue);
    }
}