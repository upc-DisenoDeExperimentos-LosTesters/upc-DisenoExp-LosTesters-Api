namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

public record ProfileResource(int id, string Name, string LastName, string Email, string Password, string Type);