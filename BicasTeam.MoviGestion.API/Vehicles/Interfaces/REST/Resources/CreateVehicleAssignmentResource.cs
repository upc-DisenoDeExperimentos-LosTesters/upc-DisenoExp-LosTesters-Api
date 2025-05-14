namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;

public record CreateVehicleAssignmentResource(int VehicleId, int TransporterId, DateTime StartDate, DateTime? EndDate, string? Route);
