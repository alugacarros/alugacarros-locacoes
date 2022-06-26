using AlugaCarros.Core.Dtos;
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

    public async Task<ResultDto<string>> CreateLocation(CreateLocationRequest createLocationRequest)
    {
        var vehicle = await _vehicleApiRepository.GetVehicleByPlate(createLocationRequest.VehiclePlate);

        var reservation = await _reservationApiRepository.GetReservationByCode(createLocationRequest.ReservationCode);

        var validationResult = ValidateRequest(vehicle, reservation);

        if (validationResult.Fail) return ResultDto<string>.Failed(validationResult.Message);

        var location = new Location(reservation.ReservationCode,
                                            reservation.AgencyCode, reservation.ClientDocument,
                                            vehicle.VehicleGroup, vehicle.Plate,
                                            reservation.PickupDate, reservation.ReturnDate,
                                            reservation.ReservationValue, reservation.SecurityDepositValue);

        await _locationRepository.AddLocation(location);

        var success = await _locationRepository.UnitOfWork.Commit();

        if (!success)
            return ResultDto<string>.Failed("Houve um erro ao criar a locação");

        await PublishCreateLocationEvents(location);
        return ResultDto<string>.Success(location.Code);
    }

    public async Task<List<LocationResponse>> GetLocations(string agencyCode)
    {
        var locations= await _locationRepository.FindLocationsByAgencyCode(agencyCode);

        return locations.Select(s => new LocationResponse(s)).ToList();
    }

    private async Task PublishCreateLocationEvents(Location location)
    {
        await _serviceBusMessagesService.Publish("compatibilizar-reservas-realizada", new { ReservationCode = location.ReservationCode });
        await _serviceBusMessagesService.Publish("compatibilizar-status-carro-locado", new { VehiclePlate = location.VehiclePlate });
    }

    private ResultDto ValidateRequest(VehicleResponse vehicle, ReservationsResponse reservation)
    {
        if (vehicle == null) return ResultDto.Failed("Veículo não encontrado!");
        if (reservation == null) return ResultDto.Failed("Reserva não encontrada!");

        if (reservation.AgencyCode != vehicle.AgencyCode) return ResultDto.Failed("Veículo selecionado não está locado para esta agência!");

        if (reservation.Status != ReservationStatus.Opened) return ResultDto.Failed("Não é possível abrir uma locação para esta reserva. Verifique o status da mesma");
        if (vehicle.Status != VehicleStatus.Available) return ResultDto.Failed("Não é possível abrir uma locação para este veículo. Verifique o status do mesmo");

        return ResultDto.Success();
    }
}