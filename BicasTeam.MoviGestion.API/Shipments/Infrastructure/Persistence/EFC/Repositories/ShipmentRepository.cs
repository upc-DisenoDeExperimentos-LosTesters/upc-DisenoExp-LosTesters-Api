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

    public async Task<IEnumerable<Shipment>> FindByTransporterIdAsync(int transporterId)
    {
        // 1. Obtener vehículos activos asignados a ese transportista  
        var vehicleIds = await Context.VehicleAssignments
            .Where(a => a.TransporterId == transporterId && (!a.EndDate.HasValue || a.EndDate >= DateTime.UtcNow))
            .Select(a => a.VehicleId)
            .ToListAsync();

        // 2. Obtener los shipments que tengan esos vehículos asignados  
        return await Context.Set<Shipment>()
            .Include(s => s.Vehicle)
            .Where(s => vehicleIds.Contains(s.VehicleId))
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

    public new async Task<Shipment?> FindByIdAsync(int id) // Se utiliza 'new' para evitar el conflicto con el método heredado
    {
        return await Context.Set<Shipment>()
            .Include(s => s.Vehicle) // necesario para obtener vehicleModel, plate, etc.  
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Shipment>> FindByUserIdAsync(int userId) // Implementación del método faltante en la interfaz
    {
        return await Context.Set<Shipment>()
            .Include(s => s.Vehicle)
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
}
