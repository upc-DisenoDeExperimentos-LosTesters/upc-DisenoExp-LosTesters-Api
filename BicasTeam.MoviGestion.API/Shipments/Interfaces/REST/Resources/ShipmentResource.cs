namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;

public record ShipmentResource(int id, int UserId, string Destiny, string Description, DateTime CreatedAt, string Status);