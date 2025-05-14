namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;
public record CreateVehicleAssignmentCommand(int VehicleId, int TransporterId, DateTime StartDate, DateTime? EndDate, string? Route);
