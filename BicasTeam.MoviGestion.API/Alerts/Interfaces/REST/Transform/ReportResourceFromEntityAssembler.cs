using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;
using BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Transform;

public class ReportResourceFromEntityAssembler
{
    public static ReportResource ToResourceFromEntity(Report entity) => new ReportResource(entity.Id, entity.Type, entity.Description, entity.UserId, entity.CreatedAt);
}