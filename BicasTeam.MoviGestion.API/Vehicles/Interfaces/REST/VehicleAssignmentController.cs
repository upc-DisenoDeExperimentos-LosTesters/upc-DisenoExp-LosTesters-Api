using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BicasTeam.MoviGestion.API.Vehicles.Interfaces.REST;

[ApiController]
[Route("api/v1/vehicles/assignments")]
[Authorize]
public class VehicleAssignmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _context;

    public VehicleAssignmentController(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> AssignVehicle([FromBody] CreateVehicleAssignmentResource resource)
    {
        var assignment = new VehicleAssignment
        {
            VehicleId = resource.VehicleId,
            TransporterId = resource.TransporterId,
            StartDate = resource.StartDate,
            EndDate = resource.EndDate,
            Route = resource.Route
        };

        _context.VehicleAssignments.Add(assignment);
        await _unitOfWork.CompleteAsync();

        return Ok(assignment);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssignments()
    {
        var assignments = await _context.VehicleAssignments
            .Include(a => a.Vehicle)
            .ToListAsync();

        return Ok(assignments);
    }

}
