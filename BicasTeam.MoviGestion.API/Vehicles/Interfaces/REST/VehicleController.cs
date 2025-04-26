using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Services;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST;

[ApiController]
[Route("/[controller]")]
public class VehicleController(
    IVehicleCommandService vehicleCommandService,
    IVehicleQueryService vehicleQueryService
): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateVehicle([FromBody] CreateVehicleResource resource)
    {
        var createVehicleCommand = CreateVehicleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await vehicleCommandService.Handle(createVehicleCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetVehicleById), new { id = result.Id}, VehicleResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetVehicleById(int id)
    {
        var getVehicleByIdQuery = new GetVehicleByIdQuery(id);
        var result = await vehicleQueryService.Handle(getVehicleByIdQuery);
        if (result is null) return NotFound();
        var resource = VehicleResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllVehicles()
    {
        var getAllVehiclesQuery = new GetAllVehiclesQuery();
        var vehicles = await vehicleQueryService.Handle(getAllVehiclesQuery);
        var resources = vehicles.Select(VehicleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}