using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Queries;

namespace BicasTeam.MoviGestion.API.Alerts.Domain.Services;

public interface IReportQueryService
{
    Task<Report?> Handle(GetReportByIdQuery query);
    Task<IEnumerable<Report>> Handle(GetAllReportsQuery query);
    Task<IEnumerable<Report>> Handle(GetReportByUserIdQuery query);
}