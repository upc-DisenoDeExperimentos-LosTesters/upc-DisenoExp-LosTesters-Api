using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Transform;

public static class CreateVehicleCommandFromResourceAssembler
{
    public static CreateVehicleCommand ToCommandFromResource(CreateVehicleResource resource) => 
        new(resource.LicensePlate, resource.Model, resource.SerialNumber, resource.IdPropietario, resource.IdTransportista);

}