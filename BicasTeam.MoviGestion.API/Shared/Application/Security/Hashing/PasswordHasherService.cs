using Microsoft.AspNetCore.Identity;

namespace BicasTeam.MoviGestion.API.Shared.Application.Security.Hashing;

public class PasswordHasherService
{
    private readonly PasswordHasher<string> _hasher = new();

    public string HashPassword(string password)
    {
        return _hasher.HashPassword("user", password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _hasher.VerifyHashedPassword("user", hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}
