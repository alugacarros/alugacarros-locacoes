using AlugaCarros.BalcaoAtendimento.Core.ServiceResponse;
using AlugaCarros.Locacoes.Domain.Entities;
using AlugaCarros.Locacoes.Domain.Interfaces;
using AlugaCarros.Locacoes.Domain.RequestResponse;
using AlugaCarros.Locacoes.Domain.RequestResponse.Reservation;
using AlugaCarros.Locacoes.Domain.RequestResponse.Vehicles;

namespace AlugaCarros.Locacoes.Domain.Services;
public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IVehicleApiRepository _vehicleApiRepository;
    private readonly IReservationApiRepository _reservationApiRepository;
    private readonly IServiceBusMessagesService _serviceBusMessagesService;

    public LocationService(ILocationRepository locationRepository,
                           IVehicleApiRepository vehicleApiRepository,
                           IReservationApiRepository reservationApiRepository, IServiceBusMessagesService serviceBusMessagesService)
    {
        _locationRepository = locationRepository;
        _vehicleApiRepository = vehicleApiRepository;
        _reservationApiRepository = reservationApiRepository;
        _serviceBusMessagesService = serviceBusMessagesService;
    }

    public async Task<ServiceResponse<string>> CreateLocation(CreateLocationRequest createLocationRequest)
    {
        var vehicle = await _vehicleApiRepository.GetVehicleByPlate(createLocationRequest.VehiclePlate);

        var reservation = await _reservationApiRepository.GetReservationByCode(createLocationRequest.ReservationCode);

        if (vehicle == null) return ServiceResponse<string>.Fail("Veículo não encontrado!");
        if (reservation == null) return ServiceResponse<string>.Fail("Reserva não encontrada!");

        if (reservation.AgencyCode != vehicle.AgencyCode) return ServiceResponse<string>.Fail("Veículo selecionado não está locado para esta agência!");

        if (reservation.Status != ReservationStatus.Opened) return ServiceResponse<string>.Fail("Não é possível abrir uma locação para esta reserva. Verifique o status da mesma");
        if (vehicle.Status != VehicleStatus.Available) return ServiceResponse<string>.Fail("Não é possível abrir uma locação para este veículo. Verifique o status do mesmo");

        var location = new Location(reservation.ReservationCode,
                                            reservation.AgencyCode, reservation.ClientDocument,
                                            vehicle.VehicleGroup, vehicle.Plate,
                                            reservation.PickupDate, reservation.ReturnDate,
                                            reservation.ReservationValue, reservation.SecurityDepositValue);

        await _locationRepository.AddLocation(location);

        var success = await _locationRepository.UnitOfWork.Commit();

        if (success)
        {
            await _serviceBusMessagesService.Publish("compatibilizar-reservas-realizada", new { ReservationCode = location.ReservationCode });
            await _serviceBusMessagesService.Publish("compatibilizar-status-carro-locado", new { VehiclePlate = location.VehiclePlate });

            return ServiceResponse<string>.Successful(location.Code, "");
        }

        return ServiceResponse<string>.Fail("Houve um erro ao criar a locação");

    }
}