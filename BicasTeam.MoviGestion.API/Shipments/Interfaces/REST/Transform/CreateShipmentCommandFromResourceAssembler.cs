using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Transform;

public static class CreateShipmentCommandFromResourceAssembler
{
    public static CreateShipmentCommand ToCommandFromResource(CreateShipmentResource resource, int userId)
    {
        return new CreateShipmentCommand(
            resource.Destiny,
            resource.Description,
            userId, // Este viene de arriba (correctamente seleccionado por el rol)
            resource.VehicleId,
            resource.Status
        );
    }

}
