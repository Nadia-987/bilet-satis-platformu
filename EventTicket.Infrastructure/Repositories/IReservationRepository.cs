using EventTicket.Domain.Entities;
using EventTicket.Domain.Enums;

namespace EventTicket.Infrastructure.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<bool> EventExistsAsync(int eventId);
    Task<bool> UserExistsAsync(int userId);
    Task<bool> SectionExistsAsync(int sectionId);
    Task<bool> SeatExistsInSectionAsync(int seatId, int sectionId);
    Task<bool> ActiveReservationExistsAsync(int eventId, int seatId);
    Task AddAsync(Reservation reservation);
    Task SaveAsync();
}