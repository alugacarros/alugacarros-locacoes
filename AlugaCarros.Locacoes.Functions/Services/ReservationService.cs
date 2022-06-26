using AlugaCarros.Locacoes.Functions.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AlugaCarros.Locacoes.Functions.Services;

public class ReservationService : IReservationService
{
    private HttpClient _httpClient;
    private readonly AuthService _authService;
    public ReservationService(IHttpClientFactory httpClientFactory, AuthService authService)
    {
        _httpClient = httpClientFactory.CreateClient("Reservation");
        _authService = authService;
    }

    public async Task<bool> CloseReservation(string reservationCode)
    {
        var token = await _authService.GetToken();

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var response = await _httpClient.PostAsync($"api/v1/reservations/{reservationCode}/close", new StringContent(""));

        return response.IsSuccessStatusCode;
    }
}