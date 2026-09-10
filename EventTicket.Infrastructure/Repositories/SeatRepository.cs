using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly AppDbContext _context;

    public SeatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Seat>> GetAllAsync()
    {
        return await _context.Seats.ToListAsync();
    }

    public async Task AddAsync(Seat seat)
    {
        await _context.Seats.AddAsync(seat);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
