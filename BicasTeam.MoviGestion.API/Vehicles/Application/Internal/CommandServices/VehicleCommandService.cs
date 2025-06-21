using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Services;

namespace BicasTeam.MoviGestion.API.Vehicles.Application.Internal.CommandServices;

public class VehicleCommandService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
    : IVehicleCommandService
{
    public async Task<Vehicle?> Handle(CreateVehicleCommand command)
    {
        // Validar si la placa ya existe
        var exists = await vehicleRepository.ExistsWithLicensePlateAsync(command.LicensePlate);
        if (exists)
        {
            // Puedes lanzar una excepción o simplemente retornar null
            throw new InvalidOperationException("Ya existe un vehículo con esa placa.");
        }

        var vehicle = new Vehicle(command);
        try
        {
            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        return vehicle;
    }

}