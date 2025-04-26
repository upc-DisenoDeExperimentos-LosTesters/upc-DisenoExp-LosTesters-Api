using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Alerts.Domain.Repositories;
using BicasTeam.MoviGestion.API.Alerts.Domain.Services;
using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;

namespace BicasTeam.MoviGestion.API.Alerts.Application.Internal.CommandServices;

public class ReportCommandService(IReportRepository reportRepository, IUnitOfWork unitOfWork)
    : IReportCommandService
{
    public async Task<Report?> Handle(CreateReportCommand command)
    {
        var report = new Report(command);
        try
        {
            await reportRepository.AddAsync(report);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        return report;
    }
}