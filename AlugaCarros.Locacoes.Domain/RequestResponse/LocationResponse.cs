using AlugaCarros.Locacoes.Domain.Entities;

namespace AlugaCarros.Locacoes.Domain.RequestResponse;
public class LocationResponse
{
    public LocationResponse(Location location)
    {
        Code = location.Code;
        ReservationCode = location.ReservationCode;
        AgencyCode = location.AgencyCode;
        ClientDocument = location.ClientDocument;
        VehicleGroupCode = location.VehicleGroupCode;
        VehiclePlate = location.VehiclePlate;
        Date = location.Date;
        ReturnDate = location.ReturnDate;
        Value = location.Value;
        SecurityDepositValue = location.SecurityDepositValue;
    }

    public string Code { get; private set; }
    public string ReservationCode { get; private set; }
    public string AgencyCode { get; set; }
    public string ClientDocument { get; set; }
    public string VehicleGroupCode { get; private set; }
    public string VehiclePlate { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime ReturnDate { get; private set; }
    public decimal Value { get; private set; }
    public decimal SecurityDepositValue { get; private set; }
}
