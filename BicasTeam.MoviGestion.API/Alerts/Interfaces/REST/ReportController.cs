using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Alerts.Domain.Services;
using BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BicasTeam.MoviGestion.API.Alerts.Interfaces.REST;

[ApiController]
[Route("api/v1/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportCommandService _reportCommandService;
    private readonly IReportQueryService _reportQueryService;

    public ReportController(
        IReportCommandService reportCommandService,
        IReportQueryService reportQueryService)
    {
        _reportCommandService = reportCommandService;
        _reportQueryService = reportQueryService;
    }

    // POST: api/v1/reports  
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateReport([FromBody] CreateReportResource resource)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized(); // 401 Unauthorized  

            var command = new CreateReportCommand(resource.Type, resource.Description, userId);
            var result = await _reportCommandService.Handle(command);

            if (result is null) return BadRequest(); // 400 Bad Request  

            var response = ReportResourceFromEntityAssembler.ToResourceFromEntity(result);
            return CreatedAtAction(nameof(GetReportById), new { id = result.Id }, response); // 201 Created  
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); // 403 Forbidden  
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed  
            return StatusCode(500, "Ocurrió un error inesperado en el sistema."); // 500 Internal Server Error  
        }
    }

    // GET: api/v1/reports/{id}  
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult> GetReportById(int id)
    {
        try
        {
            var query = new GetReportByIdQuery(id);
            var result = await _reportQueryService.Handle(query);
            if (result is null) return NotFound(); // 404 Not Found  

            var response = ReportResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(response); // 200 OK  
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); // 403 Forbidden  
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed  
            return StatusCode(500, "Ocurrió un error inesperado en el sistema."); // 500 Internal Server Error  
        }
    }

    // GET: api/v1/reports/my  
    [HttpGet("my")]
    [Authorize]
    public async Task<ActionResult> GetReportsForCurrentUser()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized(); // 401 Unauthorized  

            var query = new GetReportByUserIdQuery(userId);
            var reports = await _reportQueryService.Handle(query);
            var response = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(response); // 200 OK  
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); // 403 Forbidden  
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed  
            return StatusCode(500, "Ocurrió un error inesperado en el sistema."); // 500 Internal Server Error  
        }
    }

    // GET: api/v1/reports  
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAllReports()
    {
        try
        {
            var query = new GetAllReportsQuery();
            var reports = await _reportQueryService.Handle(query);
            var response = reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(response); // 200 OK  
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); // 403 Forbidden  
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here if needed  
            return StatusCode(500, "Ocurrió un error inesperado en el sistema."); // 500 Internal Server Error  
        }
    }
}
