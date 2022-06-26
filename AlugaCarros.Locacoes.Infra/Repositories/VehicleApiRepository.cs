using AlugaCarros.Locacoes.Domain.Interfaces;
using AlugaCarros.Locacoes.Domain.RequestResponse.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlugaCarros.Locacoes.Infra.Repositories;
public class VehicleApiRepository : IVehicleApiRepository
{
    private readonly HttpClient httpClient;

    public VehicleApiRepository(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("Vehicles");
    }

    public async Task<VehicleResponse> GetVehicleByPlate(string plate)
    {
        var response = await httpClient.GetAsync($"api/v1/vehicles/{plate}");

        if(!response.IsSuccessStatusCode) return null;

        var vehicleResponse = await response.Deserialize<VehicleResponse>();

        return vehicleResponse;
    }
}