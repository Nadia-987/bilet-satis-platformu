using EventTicket.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _reservationService.GetReservationsAsync();

        var result = reservations.Select(r => new
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
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        EventTicket.Domain.Entities.Reservation reservation)
    {
        var result = await _reservationService
            .CreateReservationAsync(reservation);

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok(result.Reservation);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteReservation(int id)
    {
        var result = await _reservationService
            .CompleteReservationAsync(id);

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok("Reservation completed successfully.");
    }
}