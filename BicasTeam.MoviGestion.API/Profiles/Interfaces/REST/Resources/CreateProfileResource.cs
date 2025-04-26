namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(string Name, string LastName, string Email, string Password, string Type);