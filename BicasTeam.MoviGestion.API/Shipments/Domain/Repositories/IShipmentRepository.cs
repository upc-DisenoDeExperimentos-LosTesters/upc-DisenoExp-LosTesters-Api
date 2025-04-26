using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;

namespace BicasTeam.MoviGestion.API.Shipments.Domain.Repositories;

public interface IShipmentRepository : IBaseRepository<Shipment>
{
    Task<IEnumerable<Shipment>> FindByUserIdAsync(int userId);
}