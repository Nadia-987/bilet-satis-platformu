using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTicketsByUser(int userId)
    {
        var result = await _ticketService.GetTicketsByUserAsync(userId);

        if (!result.Success)
            return NotFound(result.Error);

        var tickets = result.Tickets!.Select(t => new
        {
            t.Id,
            t.EventId,
            t.UserId,
            t.SeatId,
            t.SectionId,
            t.Price,
            t.Status
        });

        return Ok(tickets);
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetTicketsByEvent(int eventId)
    {
        var result = await _ticketService.GetTicketsByEventAsync(eventId);

        if (!result.Success)
            return NotFound(result.Error);

        var tickets = result.Tickets!.Select(t => new
        {
            t.Id,
            t.EventId,
            t.UserId,
            t.SeatId,
            t.SectionId,
            t.Price,
            t.Status
        });

        return Ok(tickets);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(Ticket ticket)
    {
        var result = await _ticketService.CreateTicketAsync(ticket);

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok(result.Ticket);
    }

    [HttpPut("{id}/sell")]
    public async Task<IActionResult> SellTicket(int id)
    {
        var result = await _ticketService.SellTicketAsync(id);

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok(result.Ticket);
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelTicket(int id)
    {
        var result = await _ticketService.CancelTicketAsync(id);

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok(result.Ticket);
    }
}
