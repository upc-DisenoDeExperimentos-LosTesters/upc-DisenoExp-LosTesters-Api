namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Queries;

public record GetProfileByEmailAndPasswordQuery(string Email, string Password);