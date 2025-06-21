using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Shared.Application.Security.Hashing;
using BicasTeam.MoviGestion.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

namespace BicasTeam.MoviGestion.API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService : IProfileQueryService
{
    private readonly IProfileRepository _profileRepository;
    private readonly PasswordHasherService _hasher;

    public ProfileQueryService(IProfileRepository profileRepository, PasswordHasherService hasher)
    {
        _profileRepository = profileRepository;
        _hasher = hasher;
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await _profileRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await _profileRepository.ListAsync();
    }

    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await _profileRepository.FindProfileByEmailAsync(query.Email);
    }

    public async Task<Profile?> Handle(GetProfileByEmailAndPasswordQuery query)
    {
        // Busca solo por email
        var profile = await _profileRepository.FindProfileByEmailAsync(query.Email);
        if (profile == null) return null;

        // Compara la contraseña hasheada
        var isValid = _hasher.VerifyPassword(profile.Password, query.Password);
        return isValid ? profile : null;
    }

    public async Task<IEnumerable<Profile>> GetTransportistasAsync()
    {
        return await _profileRepository.FindByRoleAsync("TRANSPORTISTA");
    }

}
