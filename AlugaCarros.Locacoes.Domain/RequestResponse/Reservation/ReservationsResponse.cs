
namespace AlugaCarros.Locacoes.Domain.RequestResponse.Reservation;
public class ReservationsResponse
{
    public string ReservationCode { get; set; }
    public string Client { get; set; }
    public string ClientDocument { get; set; }
    public string GroupCode { get; set; }
    public string GroupDescription { get; set; }
    public string AgencyCode { get; set; }
    public string AgencyName { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal ReservationValue { get; set; }
    public decimal SecurityDepositValue { get; set; }
    public ReservationStatus Status { get; set; }
}

public enum ReservationStatus
{
    Opened = 1,
    Closed = 2,
    Canceled = 3
}