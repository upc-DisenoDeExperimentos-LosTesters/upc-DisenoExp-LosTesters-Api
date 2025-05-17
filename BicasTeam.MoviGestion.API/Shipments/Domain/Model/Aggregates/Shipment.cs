using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;

namespace BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;

public partial class Shipment
{
    public int Id { get; }
    public int UserId { get; private set; }
    public string Destiny { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Status { get; private set; }
    public int VehicleId { get; private set; }
    public Vehicle? Vehicle { get; set; }

    // EF Core necesita un constructor sin parámetros, lo dejamos como privado
    private Shipment()
    {
        Destiny = string.Empty;
        Description = string.Empty;
        Status = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    public Shipment(CreateShipmentCommand command)
    {
        UserId = command.UserId;
        VehicleId = command.VehicleId;
        Destiny = command.Destiny;
        Description = command.Description;
        CreatedAt = DateTime.UtcNow;
        Status = command.Status;
    }
}
