using Tutorial6.Models;

namespace Tutorial6.Services;

public interface IAuthenticationService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Task<bool> ValidateExpiredTokenAsync(string accessToken);
}