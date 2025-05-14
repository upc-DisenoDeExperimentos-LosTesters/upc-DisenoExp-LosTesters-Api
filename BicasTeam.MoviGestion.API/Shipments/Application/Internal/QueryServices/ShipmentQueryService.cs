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
        return await shipmentRepository.FilteredListAsync(query.Status, query.StartDate, query.EndDate);
    }


    public async Task<IEnumerable<Shipment>> Handle(GetShipmentByUserIdQuery query)
    {
        return await shipmentRepository.FindByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Shipment>> Handle(GetShipmentByTransporterIdQuery query)
    {
        return await shipmentRepository.FindByTransporterIdAsync(query.TransporterId);
    }

}