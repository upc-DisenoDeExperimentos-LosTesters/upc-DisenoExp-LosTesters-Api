using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Transform;

public static class CreateShipmentCommandFromResourceAssembler
{
    public static CreateShipmentCommand ToCommandFromResource(CreateShipmentResource resource) => 
        new(resource.UserId, resource.Destiny, resource.Description, resource.Status);
}