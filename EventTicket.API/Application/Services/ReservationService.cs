using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using EventTicket.Domain.Enums;
using EventTicket.Infrastructure.Repositories;

namespace EventTicket.API.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<Reservation>> GetReservationsAsync()
    {
        return await _reservationRepository.GetAllAsync();
    }

    public async Task<(bool Success, string? Error, Reservation? Reservation)>
        CreateReservationAsync(Reservation reservation)
    {
        if (!await _reservationRepository.EventExistsAsync(reservation.EventId))
            return (false, "Event not found.", null);

        if (!await _reservationRepository.UserExistsAsync(reservation.UserId))
            return (false, "User not found.", null);

        if (!await _reservationRepository.SectionExistsAsync(reservation.SectionId))
            return (false, "Section not found.", null);

        if (reservation.SeatId.HasValue)
        {
            if (!await _reservationRepository.SeatExistsInSectionAsync(
                    reservation.SeatId.Value,
                    reservation.SectionId))
            {
                return (false, "Seat does not exist in this section.", null);
            }

            if (await _reservationRepository.ActiveReservationExistsAsync(
                    reservation.EventId,
                    reservation.SeatId.Value))
            {
                return (false, "This seat is already reserved.", null);
            }
        }

        reservation.ReservedAt = DateTime.UtcNow;
        reservation.ExpiresAt = DateTime.UtcNow.AddMinutes(10);
        reservation.Status = ReservationStatus.Active;

        await _reservationRepository.AddAsync(reservation);
        await _reservationRepository.SaveAsync();

        return (true, null, reservation);
    }

    public async Task<(bool Success, string? Error)>
        CompleteReservationAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);

        if (reservation == null)
            return (false, "Reservation not found.");

        if (reservation.Status != ReservationStatus.Active)
            return (false, "Reservation is not active.");

        reservation.Status = ReservationStatus.Completed;

        await _reservationRepository.SaveAsync();

        return (true, null);
    }
}