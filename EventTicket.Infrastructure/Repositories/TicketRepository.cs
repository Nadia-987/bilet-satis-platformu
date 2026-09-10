using EventTicket.Domain.Entities;
using EventTicket.Domain.Enums;
using EventTicket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EventExistsAsync(int eventId)
    {
        return await _context.Events.AnyAsync(e => e.Id == eventId);
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<bool> SeatExistsInSectionAsync(int seatId, int sectionId)
    {
        return await _context.Seats.AnyAsync(s =>
            s.Id == seatId && s.SectionId == sectionId);
    }

    public async Task<IEnumerable<Ticket>> GetByUserAsync(int userId)
    {
        return await _context.Tickets
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByEventAsync(int eventId)
    {
        return await _context.Tickets
            .Where(t => t.EventId == eventId)
            .ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsync(int id)
    {
        return await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> SoldTicketExistsAsync(int eventId, int seatId)
    {
        return await _context.Tickets.AnyAsync(t =>
            t.EventId == eventId &&
            t.SeatId == seatId &&
            t.Status == TicketStatus.Sold);
    }

    public async Task<Reservation?> GetActiveReservationAsync(
        int eventId,
        int userId,
        int seatId,
        int sectionId)
    {
        return await _context.Reservations
            .FirstOrDefaultAsync(r =>
                r.EventId == eventId &&
                r.UserId == userId &&
                r.SeatId == seatId &&
                r.SectionId == sectionId &&
                r.Status == ReservationStatus.Active);
    }

    public async Task AddAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
