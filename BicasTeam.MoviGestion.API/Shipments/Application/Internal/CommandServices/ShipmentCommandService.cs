using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Shipments.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shipments.Domain.Services;

namespace BicasTeam.MoviGestion.API.Shipments.Application.Internal.CommandServices;

public class ShipmentCommandService(IShipmentRepository shipmentRepository, IUnitOfWork unitOfWork)
    : IShipmentCommandService
{
    public async Task<Shipment?> Handle(CreateShipmentCommand command)
    {
        var shipment = new Shipment(command);
        try
        {
            await shipmentRepository.AddAsync(shipment);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        return shipment;
    }
}