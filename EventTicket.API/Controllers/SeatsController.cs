using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatsController : ControllerBase
{
    private readonly ISeatService _seatService;

    public SeatsController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSeats()
    {
        var seats = await _seatService.GetSeatsAsync();

        return Ok(seats);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeat(Seat seat)
    {
        var createdSeat = await _seatService.CreateSeatAsync(seat);

        return Ok(createdSeat);
    }
}