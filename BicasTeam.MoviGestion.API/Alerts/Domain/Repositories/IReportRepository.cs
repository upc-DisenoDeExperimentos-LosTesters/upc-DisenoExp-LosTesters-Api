using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;

namespace BicasTeam.MoviGestion.API.Alerts.Domain.Repositories;

public interface IReportRepository : IBaseRepository<Report>
{
    Task<IEnumerable<Report>> FindByUserIdAsync(int userId);
}

