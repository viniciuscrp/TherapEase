using System.Security.Claims;
using TherapEase.Models;
using TherapEase.Models.ViewModels;

namespace TherapEase.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserViewModel> Login(string email, string password);
        AuthTokens GenerateToken(string email);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
