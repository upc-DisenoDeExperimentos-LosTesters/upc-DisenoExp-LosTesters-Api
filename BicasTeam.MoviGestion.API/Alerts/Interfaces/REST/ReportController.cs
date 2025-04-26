using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Alerts.Domain.Services;
using BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BicasTeam.MoviGestion.API.Alerts.Interfaces.REST;

[ApiController]
[Route("/[controller]")]
public class ReportController(
    IReportCommandService reportCommandService,
    IReportQueryService reportQueryService
): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateReport([FromBody] CreateReportResource resource)
    {
        var createReportCommand = CreateReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await reportCommandService.Handle(createReportCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetReportById), new { id = result.Id}, ReportResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetReportById(int id)
    {
        var getReportByIdQuery = new GetReportByIdQuery(id);
        var result = await reportQueryService.Handle(getReportByIdQuery);
        if (result is null) return NotFound();
        var resource = ReportResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet("users/{userId}")]
    public async Task<ActionResult> GetReportByUserId(int userId)
    {
        var getAllReportByUserId = new GetReportByUserIdQuery(userId);
        var reports = await reportQueryService.Handle(getAllReportByUserId);
        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllReports()
    {
        var getAllReportsQuery = new GetAllReportsQuery();
        var reports = await reportQueryService.Handle(getAllReportsQuery);
        var resources = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}