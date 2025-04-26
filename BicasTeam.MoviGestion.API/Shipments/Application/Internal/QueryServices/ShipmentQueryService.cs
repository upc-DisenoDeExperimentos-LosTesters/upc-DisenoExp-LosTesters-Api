using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Shipments.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shipments.Domain.Services;

namespace BicasTeam.MoviGestion.API.Shipments.Application.Internal.QueryServices;

public class ShipmentQueryService(IShipmentRepository shipmentRepository) : IShipmentQueryService
{
    public async Task<Shipment?> Handle(GetShipmentByIdQuery query)
    {
        return await shipmentRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Shipment>> Handle(GetAllShipmentsQuery query)
    {
        return await shipmentRepository.ListAsync();
    }

    public async Task<IEnumerable<Shipment>> Handle(GetShipmentByUserIdQuery query)
    {
        return await shipmentRepository.FindByUserIdAsync(query.UserId);
    }
}