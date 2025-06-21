using BicasTeam.MoviGestion.API.Vehicles.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Services;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Transform;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST;

[ApiController]
[Route("api/v1/vehicles")]
[Authorize]
public class VehicleController : ControllerBase
{
    private readonly IVehicleCommandService _vehicleCommandService;
    private readonly IVehicleQueryService _vehicleQueryService;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleController(
        IVehicleCommandService vehicleCommandService,
        IVehicleQueryService vehicleQueryService,
        IVehicleRepository vehicleRepository,
        IUnitOfWork unitOfWork)
    {
        _vehicleCommandService = vehicleCommandService;
        _vehicleQueryService = vehicleQueryService;
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult> CreateVehicle([FromBody] CreateVehicleResource resource)
    {
        var command = CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await _vehicleCommandService.Handle(command);
        if (result is null) return BadRequest("Error al crear el vehículo.");
        return CreatedAtAction(nameof(GetVehicleById), new { id = result.Id }, VehicleResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetVehicleById(int id)
    {
        var query = new GetVehicleByIdQuery(id);
        var result = await _vehicleQueryService.Handle(query);
        if (result is null) return NotFound("Vehículo no encontrado.");
        return Ok(VehicleResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllVehicles()
    {
        var query = new GetAllVehiclesQuery();
        var vehicles = await _vehicleQueryService.Handle(query);
        var resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        var vehicle = await _vehicleCommandService.Handle(command);
        if (vehicle == null) return NotFound("Vehículo no encontrado.");
        return Ok(vehicle);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var vehicle = await _vehicleRepository.FindByIdAsync(id);
        if (vehicle == null) return NotFound("Vehículo no encontrado.");

        _vehicleRepository.Remove(vehicle);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}
