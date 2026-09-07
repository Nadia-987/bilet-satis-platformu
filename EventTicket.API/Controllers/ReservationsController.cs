using EventTicket.Domain.Entities;
using EventTicket.Domain.Enums;
using EventTicket.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _context.Reservations
            .Select(r => new
            {
                r.Id,
                r.EventId,
                r.UserId,
                r.SeatId,
                r.SectionId,
                r.ReservedAt,
                r.ExpiresAt,
                r.Status,

                Event = r.Event == null ? null : new
                {
                    r.Event.Id,
                    r.Event.Name,
                    r.Event.Description,
                    r.Event.StartDate
                },

                User = r.User == null ? null : new
                {
                    r.User.Id,
                    r.User.Name,
                    r.User.Email,
                    r.User.Role
                },

                Seat = r.Seat == null ? null : new
                {
                    r.Seat.Id,
                    r.Seat.Row,
                    r.Seat.Number
                },

                Section = r.Section == null ? null : new
                {
                    r.Section.Id,
                    r.Section.Name,
                    r.Section.Type,
                    r.Section.Capacity,
                    r.Section.Price
                }
            })
            .ToListAsync();

        return Ok(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(Reservation reservation)
    {
        var eventExists = await _context.Events
            .AnyAsync(e => e.Id == reservation.EventId);

        if (!eventExists)
            return BadRequest("Event not found.");

        var userExists = await _context.Users
            .AnyAsync(u => u.Id == reservation.UserId);

        if (!userExists)
            return BadRequest("User not found.");

        var seat = await _context.Seats
            .FirstOrDefaultAsync(s =>
                s.Id == reservation.SeatId &&
                s.SectionId == reservation.SectionId);

        if (seat == null)
            return BadRequest("Seat does not exist in this section.");

        var section = await _context.Sections
            .AnyAsync(s => s.Id == reservation.SectionId);

        if (!section)
            return BadRequest("Section not found.");

        var existingReservation = await _context.Reservations
            .FirstOrDefaultAsync(r =>
                r.EventId == reservation.EventId &&
                r.SeatId == reservation.SeatId &&
                r.Status == ReservationStatus.Active);

        if (existingReservation != null)
            return BadRequest("This seat is already reserved.");

        reservation.ReservedAt = DateTime.UtcNow;
        reservation.ExpiresAt = DateTime.UtcNow.AddMinutes(10);
        reservation.Status = ReservationStatus.Active;

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return Ok(reservation);
    }
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound("Reservation not found.");

        if (reservation.Status != ReservationStatus.Active)
            return BadRequest("Only active reservations can be completed.");

        reservation.Status = ReservationStatus.Completed;

        await _context.SaveChangesAsync();

        return Ok(reservation);
    }
}