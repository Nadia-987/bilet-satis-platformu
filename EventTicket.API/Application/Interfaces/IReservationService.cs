using EventTicket.Domain.Entities;

namespace EventTicket.API.Application.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetReservationsAsync();

    Task<(bool Success, string? Error, Reservation? Reservation)>
        CreateReservationAsync(Reservation reservation);

    Task<(bool Success, string? Error)>
        CompleteReservationAsync(int id);
}
