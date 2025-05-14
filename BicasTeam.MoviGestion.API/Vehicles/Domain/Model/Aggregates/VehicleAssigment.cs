namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;

public class VehicleAssignment
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int TransporterId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Route { get; set; }

    // Relaciones EF (opcional)
    public Vehicle? Vehicle { get; set; }
}
