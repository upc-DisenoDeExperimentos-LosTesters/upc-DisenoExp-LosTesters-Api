namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;

public record CreateVehicleCommand(string LicensePlate, string Model, string SerialNumber, int IdPropietario, int IdTransportista);
