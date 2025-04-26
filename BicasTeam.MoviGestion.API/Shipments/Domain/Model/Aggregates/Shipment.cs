using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;

public partial class Shipment
{
    public int Id { get; }
    public int UserId { get; private set; }
    public string Destiny { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; private set; }
    public string Status { get; set; }

    protected Shipment()
    {
        this.UserId = 2;
        this.Destiny = string.Empty;
        this.Description = string.Empty;
        this.CreatedAt = DateTime.UtcNow;
        this.Status = string.Empty;
    }

    public Shipment(CreateShipmentCommand command)
    {
        this.UserId = command.UserId;
        this.Destiny = command.Destiny;
        this.Description = command.Description;
        this.CreatedAt = DateTime.UtcNow;
        this.Status = command.Status;
    }
}