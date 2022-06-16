using AlugaCarros.Locacoes.Domain.RequestResponse.Vehicles;

namespace AlugaCarros.Locacoes.Domain.Interfaces;
public interface IVehicleApiRepository
{
    Task<VehicleResponse> GetVehicleByPlate(string plate);
}
