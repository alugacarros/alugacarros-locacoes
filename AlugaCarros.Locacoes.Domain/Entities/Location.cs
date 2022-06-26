using Bogus;

namespace AlugaCarros.Locacoes.Domain.Entities;
public class Location
{
    //EF
    protected Location()
    {

    }
    public Location(string reservationCode, string agencyCode, 
        string clientDocument, string vehicleGroupCode, string vehiclePlate, 
        DateTime pickupDate, DateTime returnDate, decimal value, decimal securityDepositValue)
    {
        var days = returnDate.DayOfYear - pickupDate.DayOfYear;
        var date = DateTime.Now;

        Id = Guid.NewGuid();
        Code = $"{agencyCode}{date.Year}-{new Faker().Random.AlphaNumeric(6).ToUpper()}"; ;
        ReservationCode = reservationCode;
        AgencyCode = agencyCode;
        ClientDocument = clientDocument;
        VehicleGroupCode = vehicleGroupCode;
        VehiclePlate = vehiclePlate;
        Date = date;
        ReturnDate = date.AddDays(days);
        Value = value;
        SecurityDepositValue = securityDepositValue;
    }

    public Guid Id { get; private set; }
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
