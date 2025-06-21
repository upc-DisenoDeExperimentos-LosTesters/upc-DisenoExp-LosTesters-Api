namespace BicasTeam.MoviGestion.API.Vehicles.Application.Internal.CommandServices
{
    public record UpdateVehicleCommand(
        int Id,
        string LicensePlate,
        string Model,
        string SerialNumber,
        int IdPropietario,
        int IdTransportista
    );
}
