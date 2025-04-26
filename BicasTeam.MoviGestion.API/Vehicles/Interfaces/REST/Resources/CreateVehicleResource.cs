namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;

public record CreateVehicleResource(string LicensePlate, string Model, string SerialNumber, int IdPropietario, int IdTransportista);
