using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Infrastructure.Repositories;

public class SectionRepository : ISectionRepository
{
    private readonly AppDbContext _context;

    public SectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Section>> GetAllAsync()
    {
        return await _context.Sections.ToListAsync();
    }

    public async Task AddAsync(Section section)
    {
        await _context.Sections.AddAsync(section);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}