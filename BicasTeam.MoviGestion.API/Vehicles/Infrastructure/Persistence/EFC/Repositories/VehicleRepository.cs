using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BicasTeam.MoviGestion.API.Vehicles.Infrastructure.Persistence.EFC.Repositories;

public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<bool> ExistsWithLicensePlateAsync(string licensePlate)
    {
        return await Context.Vehicles.AnyAsync(v => v.LicensePlate == licensePlate);
    }
    public async Task<Vehicle?> FindByIdAsync(int id)
    {
        return await Context.Vehicles.FindAsync(id);
    }

    public void Update(Vehicle vehicle)
    {
        Context.Vehicles.Update(vehicle);
    }

    public void Remove(Vehicle vehicle)
    {
        Context.Vehicles.Remove(vehicle);
    }



}