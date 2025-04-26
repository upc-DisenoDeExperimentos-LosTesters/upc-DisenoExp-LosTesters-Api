namespace BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Resources;

public record CreateReportResource(string Type, string Description, int UserId);