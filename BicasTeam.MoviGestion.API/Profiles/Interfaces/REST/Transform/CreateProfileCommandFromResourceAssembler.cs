using BicasTeam.MoviGestion.API.Profiles.Domain.Model.Commands;
using BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Resources;

namespace BicasTeam.MoviGestion.API.Profiles.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource) => 
        new(resource.Name, resource.LastName, resource.Email, resource.Password, resource.Type);
}