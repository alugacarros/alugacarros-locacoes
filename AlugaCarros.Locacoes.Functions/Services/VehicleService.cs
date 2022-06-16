using AlugaCarros.Locacoes.Functions.Services.Interfaces;

namespace AlugaCarros.Locacoes.Functions.Services;

public class VehicleService : IVehicleService
{
    private HttpClient _httpClient;
    private readonly AuthService _authService;
    public VehicleService(IHttpClientFactory httpClientFactory, AuthService authService)
    {
        _httpClient = httpClientFactory.CreateClient("Vehicle");
        _authService = authService;
    }

    public async Task<bool> RentCar(string vehiclePlate)
    {
        var token = await _authService.GetToken();

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var response = await _httpClient.PostAsync($"api/v1/vehicles/{vehiclePlate}/rent", new StringContent(""));

        return response.IsSuccessStatusCode;
    }
}

