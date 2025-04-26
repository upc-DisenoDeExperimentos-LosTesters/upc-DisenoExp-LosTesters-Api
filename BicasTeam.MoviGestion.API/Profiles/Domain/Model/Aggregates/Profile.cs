using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;

namespace BicasTeam.MoviGestion.API.Profiles.Domain.Model.Aggregates;

public class Profile
{
    public int Id { get; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Type { get; private set; }

    protected Profile()
    {
        Name = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
        Type = string.Empty;
    }

    public Profile(CreateProfileCommand command)
    {
        Name = command.Name;
        LastName = command.LastName;
        Email = command.Email;
        Password = command.Password;
        Type = command.Type;
    }
}