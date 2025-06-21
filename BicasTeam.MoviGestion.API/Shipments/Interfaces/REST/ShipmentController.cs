using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Shipments.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Shipments.Domain.Services;
using BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Shipments.Interfaces.REST.Transform;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace BicasTeam.MoviGestion.API.Shipments.Interfaces.REST;

[ApiController]
[Route("api/v1/shipments")]
[Authorize]
public class ShipmentController(
    IShipmentCommandService shipmentCommandService,
    IShipmentQueryService shipmentQueryService,
    AppDbContext context
) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateShipment([FromBody] CreateShipmentResource resource)
    {
        // Validar que el vehículo pertenezca al usuario
        var vehicle = await context.Vehicles.FindAsync(resource.VehicleId);

        if (vehicle == null || vehicle.IdPropietario != resource.UserId)
            return StatusCode(StatusCodes.Status403Forbidden,
                "Este vehículo no te pertenece o no existe.");

        var command = CreateShipmentCommandFromResourceAssembler.ToCommandFromResource(resource, resource.UserId);
        var result = await shipmentCommandService.Handle(command);
        if (result is null) return BadRequest();

        return CreatedAtAction(nameof(GetShipmentById), new { id = result.Id },
            ShipmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }









    [HttpGet("{id}")]
    public async Task<ActionResult> GetShipmentById(int id)
    {
        var query = new GetShipmentByIdQuery(id);
        var result = await shipmentQueryService.Handle(query);
        if (result is null) return NotFound();
        return Ok(ShipmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("manager/my")]
    public async Task<ActionResult> GetMyShipments()
    {
        var userId = int.Parse(User.FindFirst("id")?.Value ?? "0");
        var query = new GetShipmentByUserIdQuery(userId);
        var result = await shipmentQueryService.Handle(query);
        var resources = result.Select(ShipmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }


    [HttpGet("drivers/my-assigned")]
    public async Task<ActionResult> GetMyAssignedShipments()
    {
        var userId = int.Parse(User.FindFirst("id")?.Value ?? "0");
        var query = new GetShipmentByTransporterIdQuery(userId);
        var result = await shipmentQueryService.Handle(query);
        var resources = result.Select(ShipmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllShipments(
        [FromQuery] string? status,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var query = new GetAllShipmentsQuery(status, startDate, endDate);
        var result = await shipmentQueryService.Handle(query);
        var resources = result.Select(ShipmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipment(int id)
    {
        var shipment = await context.Shipments.FindAsync(id);
        if (shipment == null) return NotFound("Envío no encontrado.");

        context.Shipments.Remove(shipment);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateShipmentStatus(int id, [FromBody] UpdateShipmentStatusResource resource)
    {
        var shipment = await context.Shipments.FindAsync(id);
        if (shipment == null) return NotFound("Envío no encontrado.");

        shipment.GetType().GetProperty("Status")?.SetValue(shipment, resource.Status);
        await context.SaveChangesAsync();

        return Ok(ShipmentResourceFromEntityAssembler.ToResourceFromEntity(shipment));
    }


}
