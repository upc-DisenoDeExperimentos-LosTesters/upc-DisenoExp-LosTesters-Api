namespace BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;

public record CreateShipmentCommand(string Destiny, string Description, int UserId, int VehicleId, string Status);
