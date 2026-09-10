using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Repositories;

namespace EventTicket.API.Application.Services;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;

    public VenueService(IVenueRepository venueRepository)
    {
        _venueRepository = venueRepository;
    }

    public async Task<IEnumerable<Venue>> GetVenuesAsync()
    {
        return await _venueRepository.GetAllAsync();
    }

    public async Task<Venue> CreateVenueAsync(Venue venue)
    {
        await _venueRepository.AddAsync(venue);
        await _venueRepository.SaveAsync();

        return venue;
    }
}