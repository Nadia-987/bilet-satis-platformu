using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using EventTicket.Domain.Enums;
using EventTicket.Infrastructure.Repositories;

namespace EventTicket.API.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<(bool Success, string? Error, IEnumerable<Ticket>? Tickets)>
        GetTicketsByUserAsync(int userId)
    {
        if (!await _ticketRepository.UserExistsAsync(userId))
            return (false, "User not found.", null);

        var tickets = await _ticketRepository.GetByUserAsync(userId);

        return (true, null, tickets);
    }

    public async Task<(bool Success, string? Error, IEnumerable<Ticket>? Tickets)>
        GetTicketsByEventAsync(int eventId)
    {
        if (!await _ticketRepository.EventExistsAsync(eventId))
            return (false, "Event not found.", null);

        var tickets = await _ticketRepository.GetByEventAsync(eventId);

        return (true, null, tickets);
    }

    public async Task<(bool Success, string? Error, Ticket? Ticket)>
        CreateTicketAsync(Ticket ticket)
    {
        if (!await _ticketRepository.EventExistsAsync(ticket.EventId))
            return (false, "Event not found.", null);

        if (!await _ticketRepository.UserExistsAsync(ticket.UserId))
            return (false, "User not found.", null);

        if (!ticket.SeatId.HasValue)
            return (false, "Seat is required.", null);

        if (!await _ticketRepository.SeatExistsInSectionAsync(
                ticket.SeatId.Value,
                ticket.SectionId))
        {
            return (false, "Seat does not exist in this section.", null);
        }

        if (await _ticketRepository.SoldTicketExistsAsync(
                ticket.EventId,
                ticket.SeatId.Value))
        {
            return (false, "This seat is already sold.", null);
        }

        var reservation = await _ticketRepository.GetActiveReservationAsync(
            ticket.EventId,
            ticket.UserId,
            ticket.SeatId.Value,
            ticket.SectionId);

        if (reservation == null)
            return (false, "No active reservation found for this ticket.", null);

        ticket.Status = TicketStatus.Reserved;

        await _ticketRepository.AddAsync(ticket);
        await _ticketRepository.SaveAsync();

        return (true, null, ticket);
    }

    public async Task<(bool Success, string? Error, Ticket? Ticket)>
        SellTicketAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket == null)
            return (false, "Ticket not found.", null);

        if (ticket.Status != TicketStatus.Reserved)
            return (false, "Only reserved tickets can be sold.", null);

        if (!ticket.SeatId.HasValue)
            return (false, "Ticket has no seat.", null);

        var reservation = await _ticketRepository.GetActiveReservationAsync(
            ticket.EventId,
            ticket.UserId,
            ticket.SeatId.Value,
            ticket.SectionId);

        if (reservation == null)
            return (false, "No active reservation found.", null);

        ticket.Status = TicketStatus.Sold;
        reservation.Status = ReservationStatus.Completed;

        await _ticketRepository.SaveAsync();

        return (true, null, ticket);
    }

    public async Task<(bool Success, string? Error, Ticket? Ticket)>
        CancelTicketAsync(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket == null)
            return (false, "Ticket not found.", null);

        if (ticket.Status == TicketStatus.Cancelled)
            return (false, "Ticket is already cancelled.", null);

        ticket.Status = TicketStatus.Cancelled;

        await _ticketRepository.SaveAsync();

        return (true, null, ticket);
    }
}
