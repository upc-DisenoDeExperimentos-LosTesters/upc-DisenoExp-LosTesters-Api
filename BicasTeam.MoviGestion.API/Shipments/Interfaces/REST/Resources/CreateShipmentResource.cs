using System.Text.Json.Serialization;

public record CreateShipmentResource(
    [property: JsonPropertyName("destiny")] string Destiny,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("userId")] int UserId,
    [property: JsonPropertyName("vehicleId")] int VehicleId,
    [property: JsonPropertyName("status")] string Status
);
