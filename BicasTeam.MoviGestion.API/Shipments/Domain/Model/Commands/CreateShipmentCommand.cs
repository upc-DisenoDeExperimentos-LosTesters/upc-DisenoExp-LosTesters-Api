namespace BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;

public record CreateShipmentCommand(int UserId, string Destiny, string Description, string Status);