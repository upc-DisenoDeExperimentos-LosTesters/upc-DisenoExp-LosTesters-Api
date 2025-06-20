﻿using BicasTeam.MoviGestion.API.Vehicles.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Vehicles.Domain.Services;

public interface IVehicleCommandService
{
    Task<Vehicle?> Handle(CreateVehicleCommand command);
    Task<Vehicle?> Handle(UpdateVehicleCommand command);
}