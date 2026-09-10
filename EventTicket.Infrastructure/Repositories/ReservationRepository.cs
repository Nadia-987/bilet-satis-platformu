using EventTicket.Domain.Entities;
using EventTicket.Domain.Enums;
using EventTicket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.Event)
            .Include(r => r.User)
            .Include(r => r.Seat)
            .Include(r => r.Section)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<bool> EventExistsAsync(int eventId)
    {
        return await _context.Events.AnyAsync(e => e.Id == eventId);
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<bool> SectionExistsAsync(int sectionId)
    {
        return await _context.Sections.AnyAsync(s => s.Id == sectionId);
    }

    public async Task<bool> SeatExistsInSectionAsync(int seatId, int sectionId)
    {
        return await _context.Seats.AnyAsync(s =>
            s.Id == seatId && s.SectionId == sectionId);
    }

    public async Task<bool> ActiveReservationExistsAsync(int eventId, int seatId)
    {
        return await _context.Reservations.AnyAsync(r =>
            r.EventId == eventId &&
            r.SeatId == seatId &&
            r.Status == ReservationStatus.Active);
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}