using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Repositories;

namespace EventTicket.API.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();

        return user;
    }
}