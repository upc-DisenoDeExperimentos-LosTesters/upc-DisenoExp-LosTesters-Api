﻿using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindProfileByEmailAsync(string email);

    Task<Profile?> FindProfileByEmailAndPasswordAsync(string email, string password);

    // 🔥 Agrega esto:
    Task<IEnumerable<Profile>> FindByRoleAsync(string role);
}
