namespace BicasTeam.MoviGestion.API.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}