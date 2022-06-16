using AlugaCarros.Locacoes.Domain.Interfaces;
using AlugaCarros.Locacoes.Domain.RequestResponse.Reservation;

namespace AlugaCarros.Locacoes.Infra.Repositories;

public class ReservationApiRepository : IReservationApiRepository
{
    private readonly HttpClient httpClient;

    public ReservationApiRepository(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("Reservations");
    }

    public async Task<ReservationsResponse> GetReservationByCode(string code)
    {
        var response = await httpClient.GetAsync($"api/v1/reservations/{code}");

        if (!response.IsSuccessStatusCode) return null;

        var reservationResponse = await response.Deserialize<ReservationsResponse>();

        return reservationResponse;
    }
}