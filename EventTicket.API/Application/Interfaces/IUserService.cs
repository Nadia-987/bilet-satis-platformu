using EventTicket.Domain.Entities;

namespace EventTicket.API.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> CreateUserAsync(User user);
}