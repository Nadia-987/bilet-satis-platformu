using EventTicket.Domain.Entities;

namespace EventTicket.API.Application.Interfaces;

public interface ITicketService
{
    Task<(bool Success, string? Error, IEnumerable<Ticket>? Tickets)>
        GetTicketsByUserAsync(int userId);

    Task<(bool Success, string? Error, IEnumerable<Ticket>? Tickets)>
        GetTicketsByEventAsync(int eventId);

    Task<(bool Success, string? Error, Ticket? Ticket)>
        CreateTicketAsync(Ticket ticket);

    Task<(bool Success, string? Error, Ticket? Ticket)>
        SellTicketAsync(int id);

    Task<(bool Success, string? Error, Ticket? Ticket)>
        CancelTicketAsync(int id);
}