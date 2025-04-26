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
        return await Context.Set<Shipment>().Where(f => f.UserId == userId).ToListAsync();
    }
}