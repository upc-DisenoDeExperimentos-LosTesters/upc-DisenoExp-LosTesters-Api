using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BicasTeam.MoviGestion.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
{
    public ProfileRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Profile?> FindProfileByEmailAsync(string email)
    {
        return await Context.Set<Profile>().Where(p => p.Email == email).FirstOrDefaultAsync();
    }

    public async Task<Profile?> FindProfileByEmailAndPasswordAsync(string email, string password)
    {
        return await Context.Set<Profile>().Where(p => p.Email == email && p.Password == password).FirstOrDefaultAsync();
    }
}