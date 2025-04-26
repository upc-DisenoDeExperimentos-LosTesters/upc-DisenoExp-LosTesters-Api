using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Shipments.Domain.Services;
using BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST;

[ApiController]
[Route("/[controller]")]
public class ShipmentController(
    IShipmentCommandService shipmentCommandService,
    IShipmentQueryService shipmentQueryService
): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateShipment([FromBody] CreateShipmentResource resource)
    {

        var createShipmentCommand = CreateShipmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await shipmentCommandService.Handle(createShipmentCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetShipmentById), new { id = result.Id}, ShipmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetShipmentById(int id)
    {
        var getShipmentByIdQuery = new GetShipmentByIdQuery(id);
        var result = await shipmentQueryService.Handle(getShipmentByIdQuery);
        if (result is null) return NotFound();
        var resource = ShipmentResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet("users/{userId}")]
    public async Task<ActionResult> GetShipmentByUserId(int userId)
    {
        var getAllShipmentByUserId = new GetShipmentByUserIdQuery(userId);
        var shipments = await shipmentQueryService.Handle(getAllShipmentByUserId);
        var resources = shipments.Select(ShipmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllReports()
    {
        var getAllShipmentsQuery = new GetAllShipmentsQuery();
        var shipments = await shipmentQueryService.Handle(getAllShipmentsQuery);
        var resources = shipments.Select(ShipmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}