using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST;

[ApiController]
[Route("/[controller]")]
public class ProfileController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService
): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateProfile([FromBody] CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await profileCommandService.Handle(createProfileCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetProfileById), new { id = result.Id}, ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetProfileById(int id)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(id);
        var result = await profileQueryService.Handle(getProfileByIdQuery);
        if (result is null) return NotFound();
        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllProfiles()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(getAllProfilesQuery);
        var resources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpGet("email/{email}")]
    public async Task<ActionResult> GetProfileByEmail(string email)
    {
        var getProfileByEmailQuery = new GetProfileByEmailQuery(email);
        var result = await profileQueryService.Handle(getProfileByEmailQuery);
        if (result is null) return NotFound();
        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpGet("email/{email}/password/{password}")]
    public async Task<ActionResult> GetProfileByEmailAndPassword(string email, string password)
    {
        var getProfileByEmailAndPasswordQuery = new GetProfileByEmailAndPasswordQuery(email, password);
        var result = await profileQueryService.Handle(getProfileByEmailAndPasswordQuery);
        if (result is null) return NotFound();
        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}