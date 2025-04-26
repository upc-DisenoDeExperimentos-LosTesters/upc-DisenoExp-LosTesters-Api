using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Queries;

namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Services;

public interface IVehicleQueryService
{
    Task<Vehicle?> Handle(GetVehicleByIdQuery query);
    Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query);
}