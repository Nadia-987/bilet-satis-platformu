using EventTicket.Domain.Entities;

namespace EventTicket.Infrastructure.Repositories;

public interface ISeatRepository
{
    Task<IEnumerable<Seat>> GetAllAsync();
    Task AddAsync(Seat seat);
    Task SaveAsync();
}