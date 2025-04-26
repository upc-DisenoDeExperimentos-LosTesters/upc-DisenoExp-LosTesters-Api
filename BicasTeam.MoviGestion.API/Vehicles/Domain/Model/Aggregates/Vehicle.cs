using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;

public class Vehicle
{
    public int Id { get; set; }
    public string LicensePlate { get; private set; }
    public string Model { get; private set; }
    public string SerialNumber { get; private set; }
    public int IdPropietario { get; private set; }  // Nuevo atributo
    public int IdTransportista { get; private set; }  // Nuevo atributo

    protected Vehicle()
    {
        LicensePlate = string.Empty;
        Model = string.Empty;
        SerialNumber = string.Empty;
        IdPropietario = 0;
        IdTransportista = 0;
    }
    
    public Vehicle(CreateVehicleCommand command)
    {
        LicensePlate = command.LicensePlate;
        Model = command.Model;
        SerialNumber = command.SerialNumber;
        IdPropietario = command.IdPropietario;  // Nuevo atributo
        IdTransportista = command.IdTransportista;  // Nuevo atributo
    }
}