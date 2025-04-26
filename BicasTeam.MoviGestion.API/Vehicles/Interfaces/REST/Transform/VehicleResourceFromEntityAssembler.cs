using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Transform;

public class VehicleResourceFromEntityAssembler
{
    public static VehicleResource ToResourceFromEntity(Vehicle entity) => 
        new(entity.Id, entity.LicensePlate, entity.Model, entity.SerialNumber, entity.IdPropietario, entity.IdTransportista);

}