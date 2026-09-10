using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VenuesController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenuesController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [HttpGet]
    public async Task<IActionResult> GetVenues()
    {
        var venues = await _venueService.GetVenuesAsync();

        var result = venues.Select(v => new
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
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVenue(Venue venue)
    {
        var createdVenue = await _venueService.CreateVenueAsync(venue);

        return Ok(createdVenue);
    }
}