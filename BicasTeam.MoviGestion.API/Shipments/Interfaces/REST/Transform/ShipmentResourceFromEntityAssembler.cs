using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Transform;

public class ShipmentResourceFromEntityAssembler
{
    public static ShipmentResource ToResourceFromEntity(Shipment entity) => new ShipmentResource(entity.Id, entity.UserId, entity.Destiny, 
        entity.Description, entity.CreatedAt, entity.Status);
}