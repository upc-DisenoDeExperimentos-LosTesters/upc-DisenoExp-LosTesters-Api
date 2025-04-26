using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Queries;
using BicasTeam.MoviGestion.API.Alerts.Domain.Repositories;
using BicasTeam.MoviGestion.API.Alerts.Domain.Services;

namespace BicasTeam.MoviGestion.API.Alerts.Application.Internal.QueryServices;

public class ReportQueryService(IReportRepository reportRepository) : IReportQueryService
{
    public async Task<Report?> Handle(GetReportByIdQuery query)
    {
        return await reportRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Report>> Handle(GetAllReportsQuery query)
    {
        return await reportRepository.ListAsync();
    }

    public async Task<IEnumerable<Report>> Handle(GetReportByUserIdQuery query)
    {
        return await reportRepository.FindByUserIdAsync(query.UserId);
    }
}
