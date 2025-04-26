using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Queries;

namespace BicasTeam.MoviGestion.API.Shipments.Domain.Services;

public interface IShipmentQueryService
{
    Task<Shipment?> Handle(GetShipmentByIdQuery query);
    Task<IEnumerable<Shipment>> Handle(GetAllShipmentsQuery query);
    Task<IEnumerable<Shipment>> Handle(GetShipmentByUserIdQuery query);
}