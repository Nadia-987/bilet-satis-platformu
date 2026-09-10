using EventTicket.Domain.Entities;

namespace EventTicket.Infrastructure.Repositories;

public interface ITicketRepository
{
    Task<bool> EventExistsAsync(int eventId);
    Task<bool> UserExistsAsync(int userId);
    Task<bool> SeatExistsInSectionAsync(int seatId, int sectionId);

    Task<IEnumerable<Ticket>> GetByUserAsync(int userId);
    Task<IEnumerable<Ticket>> GetByEventAsync(int eventId);

    Task<Ticket?> GetByIdAsync(int id);

    Task<bool> SoldTicketExistsAsync(int eventId, int seatId);

    Task<Reservation?> GetActiveReservationAsync(
        int eventId,
        int userId,
        int seatId,
        int sectionId);

    Task AddAsync(Ticket ticket);
    Task SaveAsync();
}
