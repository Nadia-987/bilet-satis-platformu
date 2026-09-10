
using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _context.Events
            .Include(e => e.Venue)
                .ThenInclude(v => v.Sections)
                    .ThenInclude(s => s.Seats)
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Venue)
                .ThenInclude(v => v.Sections)
                    .ThenInclude(s => s.Seats)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Event eventItem)
    {
        await _context.Events.AddAsync(eventItem);
    }

    public async Task DeleteAsync(Event eventItem)
    {
        _context.Events.Remove(eventItem);
        await Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
