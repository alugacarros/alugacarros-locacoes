using AlugaCarros.Locacoes.Domain.RequestResponse.Reservation;

namespace AlugaCarros.Locacoes.Domain.Interfaces;

public interface IReservationApiRepository
{
    Task<ReservationsResponse> GetReservationByCode(string code);
}