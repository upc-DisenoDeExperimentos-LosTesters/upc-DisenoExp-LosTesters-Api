namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;

public record ShipmentResource(
    int Id,
    int userId,
    string Destiny,
    string Description,
    string Status,
    DateTime CreatedAt,
    int VehicleId,
    string? VehicleModel,
    string? VehiclePlate,
    int? TransporterId
);
