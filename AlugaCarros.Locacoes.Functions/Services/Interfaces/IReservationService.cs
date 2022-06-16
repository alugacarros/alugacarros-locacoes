namespace AlugaCarros.Locacoes.Functions.Services.Interfaces;
public interface IReservationService
{
    Task<bool> CloseReservation(string reservationCode);
}