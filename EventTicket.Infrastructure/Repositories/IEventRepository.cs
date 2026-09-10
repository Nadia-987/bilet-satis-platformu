using EventTicket.Domain.Entities;

namespace EventTicket.Infrastructure.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
    Task AddAsync(Event eventItem);
    Task DeleteAsync(Event eventItem);
    Task SaveAsync();
}
