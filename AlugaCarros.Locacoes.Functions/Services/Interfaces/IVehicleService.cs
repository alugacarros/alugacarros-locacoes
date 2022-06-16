namespace AlugaCarros.Locacoes.Functions.Services.Interfaces;
public interface IVehicleService
{
    Task<bool> RentCar(string vehiclePlate);
}
