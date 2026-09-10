using EventTicket.Domain.Entities;

namespace EventTicket.API.Application.Interfaces;

public interface IVenueService
{
    Task<IEnumerable<Venue>> GetVenuesAsync();
    Task<Venue> CreateVenueAsync(Venue venue);
}