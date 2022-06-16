namespace AlugaCarros.Locacoes.Domain.RequestResponse.Vehicles;
public class VehicleResponse
{
    public string AgencyCode { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public string VehicleGroup { get; set; }
    public string VehicleGroupDescription { get; set; }
    public VehicleStatus Status { get; set; }
}

public enum VehicleStatus
{
    Available = 1,
    Rented = 2,
    InMaintenance = 3,
    Inactive = 4
}
