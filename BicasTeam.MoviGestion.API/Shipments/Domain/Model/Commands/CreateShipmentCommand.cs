namespace BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;

public class CreateShipmentCommand
{
    public string Destiny { get; }
    public string Description { get; }
    public int UserId { get; }
    public int VehicleId { get; }
    public string Status { get; }

    public CreateShipmentCommand(string destiny, string description, int userId, int vehicleId, string status)
    {
        Destiny = destiny;
        Description = description;
        UserId = userId;
        VehicleId = vehicleId;
        Status = status;
    }
}