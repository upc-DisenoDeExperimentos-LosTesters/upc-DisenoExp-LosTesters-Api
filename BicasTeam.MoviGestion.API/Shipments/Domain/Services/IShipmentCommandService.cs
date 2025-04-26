using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Shipments.Domain.Services;

public interface IShipmentCommandService
{
    Task<Shipment?> Handle(CreateShipmentCommand command);
}