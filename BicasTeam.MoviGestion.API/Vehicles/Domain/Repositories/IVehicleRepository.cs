using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using BicasTeam.MoviGestion.API.Vehicles.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;

namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Repositories;

public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    Task<bool> ExistsWithLicensePlateAsync(string licensePlate);
    Task<Vehicle?> FindByIdAsync(int id);
    void Update(Vehicle vehicle);
    void Remove(Vehicle vehicle);


}