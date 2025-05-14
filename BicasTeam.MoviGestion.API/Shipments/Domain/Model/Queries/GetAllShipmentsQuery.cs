namespace BicasTeam.MoviGestion.API.Shipments.Domain.Model.Queries;

public record GetAllShipmentsQuery(string? Status = null, DateTime? StartDate = null, DateTime? EndDate = null);
