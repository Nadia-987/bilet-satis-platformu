using EventTicket.Domain.Entities;

namespace EventTicket.Infrastructure.Repositories;

public interface ISectionRepository
{
    Task<IEnumerable<Section>> GetAllAsync();
    Task AddAsync(Section section);
    Task SaveAsync();
}
