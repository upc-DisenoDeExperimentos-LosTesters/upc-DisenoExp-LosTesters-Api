namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;

public record CreateShipmentResource(string Destiny, string Description, int VehicleId, string Status);
