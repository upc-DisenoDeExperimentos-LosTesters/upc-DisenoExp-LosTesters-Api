using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Services;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Transform;
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

    public VehicleController(
        IVehicleCommandService vehicleCommandService,
        IVehicleQueryService vehicleQueryService)
    {
        _vehicleCommandService = vehicleCommandService;
        _vehicleQueryService = vehicleQueryService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateVehicle([FromBody] CreateVehicleResource resource)
    {
        var command = CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await _vehicleCommandService.Handle(command);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetVehicleById), new { id = result.Id }, VehicleResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetVehicleById(int id)
    {
        var query = new GetVehicleByIdQuery(id);
        var result = await _vehicleQueryService.Handle(query);
        if (result is null) return NotFound();
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
}
