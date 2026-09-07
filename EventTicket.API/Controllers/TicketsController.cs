using EventTicket.Domain.Entities;
using EventTicket.Domain.Enums;
using EventTicket.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTicketsByUser(int userId)
    {
        var userExists = await _context.Users
            .AnyAsync(u => u.Id == userId);

        if (!userExists)
            return NotFound("User not found.");

        var tickets = await _context.Tickets
            .Where(t => t.UserId == userId)
            .Select(t => new
            {
                t.Id,
                t.EventId,
                t.UserId,
                t.SeatId,
                t.SectionId,
                t.Price,
                t.Status
            })
            .ToListAsync();

        return Ok(tickets);
    }
    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetTicketsByEvent(int eventId)
    {
        var eventExists = await _context.Events
            .AnyAsync(e => e.Id == eventId);

        if (!eventExists)
            return NotFound("Event not found.");

        var tickets = await _context.Tickets
            .Where(t => t.EventId == eventId)
            .Select(t => new
            {
                t.Id,
                t.EventId,
                t.UserId,
                t.SeatId,
                t.SectionId,
                t.Price,
                t.Status
            })
            .ToListAsync();

        return Ok(tickets);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(Ticket ticket)
    {
        var eventExists = await _context.Events
            .AnyAsync(e => e.Id == ticket.EventId);

        if (!eventExists)
            return BadRequest("Event not found.");

        var userExists = await _context.Users
            .AnyAsync(u => u.Id == ticket.UserId);

        if (!userExists)
            return BadRequest("User not found.");

        var seat = await _context.Seats
            .FirstOrDefaultAsync(s =>
                s.Id == ticket.SeatId &&
                s.SectionId == ticket.SectionId);

        if (seat == null)
            return BadRequest("Seat does not exist in this section.");

        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r =>
                r.EventId == ticket.EventId &&
                r.UserId == ticket.UserId &&
                r.SeatId == ticket.SeatId &&
                r.SectionId == ticket.SectionId &&
                r.Status == ReservationStatus.Active);

        var soldTicket = await _context.Tickets
            .FirstOrDefaultAsync(t =>
                t.EventId == ticket.EventId &&
                t.SeatId == ticket.SeatId &&
                t.Status == TicketStatus.Sold);

        if (soldTicket != null)
            return BadRequest("This seat is already sold.");

        if (reservation == null)
            return BadRequest("No active reservation found for this ticket.");

        ticket.Status = TicketStatus.Reserved;

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return Ok(ticket);
    }
    [HttpPut("{id}/sell")]
    public async Task<IActionResult> SellTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
            return NotFound("Ticket not found.");

        if (ticket.Status != TicketStatus.Reserved)
            return BadRequest("Only reserved tickets can be sold.");

        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r =>
                r.EventId == ticket.EventId &&
                r.UserId == ticket.UserId &&
                r.SeatId == ticket.SeatId &&
                r.SectionId == ticket.SectionId &&
                r.Status == ReservationStatus.Active);

        if (reservation == null)
            return BadRequest("No active reservation found.");

        ticket.Status = TicketStatus.Sold;
        reservation.Status = ReservationStatus.Completed;

        await _context.SaveChangesAsync();

        return Ok(ticket);
    }
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelTicket(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
            return NotFound("Ticket not found.");

        if (ticket.Status == TicketStatus.Cancelled)
            return BadRequest("Ticket is already cancelled.");

        ticket.Status = TicketStatus.Cancelled;

        await _context.SaveChangesAsync();

        return Ok(ticket);
    }
}
