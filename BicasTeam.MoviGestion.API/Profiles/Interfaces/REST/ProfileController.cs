using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;
using BicasTeam.MoviGestion.API.Shared.Application.Security.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileCommandService _profileCommandService;
    private readonly IProfileQueryService _profileQueryService;
    private readonly JwtService _jwtService;

    public ProfileController(
        IProfileCommandService profileCommandService,
        IProfileQueryService profileQueryService,
        JwtService jwtService)
    {
        _profileCommandService = profileCommandService;
        _profileQueryService = profileQueryService;
        _jwtService = jwtService;
    }

    // POST: api/v1/profile
    [HttpPost]
    [AllowAnonymous] // Registro público
    public async Task<ActionResult> CreateProfile([FromBody] CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await _profileCommandService.Handle(createProfileCommand);
        if (result is null) return BadRequest();

        var resourceResult = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetProfileById), new { id = result.Id }, resourceResult);
    }

    // GET: api/v1/profile/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult> GetProfileById(int id)
    {
        var query = new GetProfileByIdQuery(id);
        var result = await _profileQueryService.Handle(query);
        if (result is null) return NotFound();

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    // GET: api/v1/profile
    [HttpGet]
    [Authorize(Roles = "GERENTE")] // Solo administradores pueden listar todos
    public async Task<ActionResult> GetAllProfiles()
    {
        var query = new GetAllProfilesQuery();
        var result = await _profileQueryService.Handle(query);

        var resources = result.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    // GET: api/v1/profile/email/{email}
    [HttpGet("email/{email}")]
    [Authorize]
    public async Task<ActionResult> GetProfileByEmail(string email)
    {
        var query = new GetProfileByEmailQuery(email);
        var result = await _profileQueryService.Handle(query);
        if (result is null) return NotFound();

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    // POST: api/v1/profile/login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginResource login)
    {
        var query = new GetProfileByEmailAndPasswordQuery(login.Email, login.Password);
        var profile = await _profileQueryService.Handle(query);

        if (profile == null) return Unauthorized(new { message = "Credenciales inválidas" });

        var token = _jwtService.GenerateToken(profile.Id, profile.Email, profile.Type);
        return Ok(new { Token = token });
    }

    [HttpGet("transportistas")]
    public async Task<IActionResult> GetTransportistas()
    {
        var list = await _profileQueryService.GetTransportistasAsync();
        return Ok(list.Select(p => new {
            id = p.Id,
            name = p.Name // o combinar nombre + apellido si lo tienes
        }));
    }
}
