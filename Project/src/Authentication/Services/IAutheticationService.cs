using Project.Authentication.Models;

namespace Project.Authentication.Services;

public interface IAuthenticationService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Task<bool> ValidateExpiredTokenAsync(string accessToken);
}