using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Shared.Application.Security.Hashing;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;

namespace BicasTeam.MoviGestion.API.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService : IProfileCommandService
{
    private readonly IProfileRepository _profileRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHasherService _hasher;

    public ProfileCommandService(
        IProfileRepository profileRepository,
        IUnitOfWork unitOfWork,
        PasswordHasherService hasher)
    {
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
        _hasher = hasher;
    }

    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        try
        {
            // hashing
            var hashedPassword = _hasher.HashPassword(command.Password);

            
            var secureCommand = new CreateProfileCommand(
                command.Name,
                command.LastName,
                command.Email,
                hashedPassword,
                command.Type
            );

            var profile = new Profile(secureCommand);
            await _profileRepository.AddAsync(profile);
            await _unitOfWork.CompleteAsync();

            return profile;
        }
        catch
        {
            return null;
        }
    }
}
