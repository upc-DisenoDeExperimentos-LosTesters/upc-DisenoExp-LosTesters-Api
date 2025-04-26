namespace BicasTeam.MoviGestion.API.Alerts.Domain.Model.Commands;

public record CreateReportCommand(string Type, string Description, int UserId);

