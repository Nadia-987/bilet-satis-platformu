using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SeatsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSeats()
    {
        var seats = await _context.Seats.ToListAsync();

        return Ok(seats);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeat(Seat seat)
    {
        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();

        return Ok(seat);
    }
}