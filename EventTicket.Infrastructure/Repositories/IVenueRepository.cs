using EventTicket.Domain.Entities;

namespace EventTicket.Infrastructure.Repositories;

public interface IVenueRepository
{
    Task<IEnumerable<Venue>> GetAllAsync();
    Task AddAsync(Venue venue);
    Task SaveAsync();
}
