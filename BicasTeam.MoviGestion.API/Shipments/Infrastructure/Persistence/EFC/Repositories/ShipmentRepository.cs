using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shipments.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BicasTeam.MoviGestion.API.Shipments.Infrastructure.Persistence.EFC.Repositories;

public class ShipmentRepository : BaseRepository<Shipment>, IShipmentRepository
{
    public ShipmentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Shipment>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Shipment>()
            .Include(s => s.Vehicle)
            .Where(f => f.UserId == userId)
            .ToListAsync();
    }


    public async Task<IEnumerable<Shipment>> FilteredListAsync(string? status, DateTime? startDate, DateTime? endDate)
    {
        var shipments = Context.Set<Shipment>().AsQueryable();

        if (!string.IsNullOrEmpty(status))
            shipments = shipments.Where(s => s.Status == status);

        if (startDate.HasValue)
            shipments = shipments.Where(s => s.CreatedAt >= startDate.Value);

        if (endDate.HasValue)
            shipments = shipments.Where(s => s.CreatedAt <= endDate.Value);

        return await shipments
            .Include(s => s.Vehicle)
            .ToListAsync();
    }

    public async Task<Shipment?> FindByIdAsync(int id)
    {
        return await Context.Set<Shipment>()
            .Include(s => s.Vehicle) // necesario para obtener vehicleModel, plate, etc.
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Shipment>> FindByTransporterIdAsync(int transporterId)
    {
        return await Context.Set<Shipment>()
            .Include(s => s.Vehicle)
            .Where(s => s.Vehicle != null && s.Vehicle.IdTransportista == transporterId)
            .ToListAsync();
    }

}