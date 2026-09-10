using EventTicket.Domain.Entities;

namespace EventTicket.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task SaveAsync();
}
