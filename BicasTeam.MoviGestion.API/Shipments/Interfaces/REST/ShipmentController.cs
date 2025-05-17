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
        // Validación de asignación: que el vehículo esté asignado al transportista indicado
        var isAssigned = await context.VehicleAssignments.AnyAsync(a =>
            a.VehicleId == resource.VehicleId &&
            a.TransporterId == resource.UserId &&
            (!a.EndDate.HasValue || a.EndDate >= DateTime.UtcNow)
        );

        if (!isAssigned)
            return StatusCode(StatusCodes.Status403Forbidden,
                "Este vehículo no está asignado al transportista indicado o la asignación expiró.");

        // Crea el comando usando el UserId recibido directamente del body
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
}
