namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;

public record CreateShipmentResource(int UserId, string Destiny, string Description, string Status);