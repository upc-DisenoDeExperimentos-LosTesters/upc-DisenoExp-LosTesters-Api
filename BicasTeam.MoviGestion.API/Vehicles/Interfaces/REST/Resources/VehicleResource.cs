namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;

public record VehicleResource(int Id, string LicensePlate, string Model, string SerialNumber, int IdPropietario, int IdTransportista);
