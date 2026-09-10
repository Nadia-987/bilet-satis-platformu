using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Infrastructure.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly AppDbContext _context;

    public VenueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Venue>> GetAllAsync()
    {
        return await _context.Venues
            .Include(v => v.Sections)
                .ThenInclude(s => s.Seats)
            .ToListAsync();
    }

    public async Task AddAsync(Venue venue)
    {
        await _context.Venues.AddAsync(venue);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
