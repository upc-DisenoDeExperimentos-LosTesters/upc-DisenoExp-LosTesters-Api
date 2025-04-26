namespace BicasTeam.MoviGestion.API.Alerts.Interfaces.REST.Resources;

public record ReportResource(int id, string Type, string Description, int UserId, DateTime CreatedAt);