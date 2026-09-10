using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Repositories;

namespace EventTicket.API.Application.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;

    public SeatService(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    public async Task<IEnumerable<Seat>> GetSeatsAsync()
    {
        return await _seatRepository.GetAllAsync();
    }

    public async Task<Seat> CreateSeatAsync(Seat seat)
    {
        await _seatRepository.AddAsync(seat);
        await _seatRepository.SaveAsync();

        return seat;
    }
}
