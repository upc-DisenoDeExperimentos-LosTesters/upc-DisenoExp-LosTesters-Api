using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Services;

namespace BicasTeam.MoviGestion.API.Vehicles.Application.Internal.QueryServices;

public class VehicleQueryService(IVehicleRepository vehicleRepository) : IVehicleQueryService
{
    public async Task<Vehicle?> Handle(GetVehicleByIdQuery query)
    {
        return await vehicleRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query)
    {
        return await vehicleRepository.ListAsync();
    }
}