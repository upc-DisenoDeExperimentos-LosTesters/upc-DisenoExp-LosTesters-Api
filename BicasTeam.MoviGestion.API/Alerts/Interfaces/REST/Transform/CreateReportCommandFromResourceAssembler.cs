using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Transform;

public static class CreateReportCommandFromResourceAssembler
{
    public static CreateReportCommand ToCommandFromResource(CreateReportResource resource) => 
        new(resource.Type, resource.Description, resource.UserId);
}