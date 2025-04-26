using BicasTeam.MoviGestion.API.Alerts.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Alerts.Domain.Model.Aggregates;

public partial class Report
{
    public int Id { get; }
    public int UserId { get; private set; }
    public string Type { get; private set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; private set; }
    
    protected Report()
    {
        this.UserId = 2;
        this.Type = string.Empty;
        this.Description = String.Empty;
        this.CreatedAt = DateTime.UtcNow;
    }

    public Report(CreateReportCommand command)
    {
        this.UserId = command.UserId;
        this.Type = command.Type;
        this.Description = command.Description;
        this.CreatedAt = DateTime.UtcNow;
    }
}